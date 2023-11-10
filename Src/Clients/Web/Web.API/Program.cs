using Microsoft.AspNetCore.HttpOverrides;
using Web.API.Extensions;
using Web.API.Extensions.CorePolicies;
using Web.API.Extensions.HttpClient;
using Web.API.Middlewares;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

// Add Configuration from json files
builder.ReadConfigurations(env);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============== Management Request Headers in Linux Host ==============
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// ============== allow Cors Policy ==============
builder.Services.ConfigureCorePolices();


// ======== Add Default Http Client Service  ========
builder.Services.ConfigureHttpClientsApp(builder.Configuration);


// ========================================= Middleware =========================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ============== Management Request Headers in Linux Host ==============
app.UseForwardedHeaders();

// ============== My Global Exception ==============
app.UseExceptionMiddleware();

// ============== Reforce use https ============== 
// app.UseHttpsRedirection();

// ============== enable routing for controller ==============
app.UseRouting();

// ============== allow Cors Policy ==============
app.UseCors("CorsPolicy");

// ============== Authentication & Authorization ==============
// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await app.RunAsync();

