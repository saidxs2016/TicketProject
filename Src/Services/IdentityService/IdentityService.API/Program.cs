using IdentityService.API.Extensions.Auth;
using IdentityService.API.Extensions;

using Microsoft.AspNetCore.HttpOverrides;
using IdentityService.API.Extensions.CorePolicies;
using IdentityService.API.Middlewares;
using IdentityService.API.Extensions.ServiceDiscovery;
using IdentityService.Application;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

// Add Configuration from json files
builder.ReadConfigurations(env);

// Add services to the container.
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();

// ============== Management Request Headers in Linux Host ==============
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// ============== allow Cors Policy ==============
builder.Services.ConfigureCorePolicies();

// ============== Auth - JWT ==============
builder.Services.ConfigureAuth(builder.Configuration);

// ======== Add Service Discovery and Consul ========
builder.Services.AddServiceDiscoveryRegistration(builder.Configuration);

// ======== Add External Application Services  ========
builder.Services.AddApplicationServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// ========================================= Middleware =========================================
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}



// ============== Management Request Headers in Linux Host ==============
app.UseForwardedHeaders();

// ============== My Global Exception ==============
app.UseExceptionMiddleware();

// ============== Reforce use https ============== 
//app.UseHttpsRedirection();

// ============== enable routing for controller ==============
app.UseRouting();

// ============== allow Cors Policy ==============
app.UseCors("CorsPolicy");

// ============== Authentication & Authorization ==============
//app.UseAuthentication();
app.UseAuthorization();


// ======== Register Consul Service  ========
app.RegisterWithConsul(app.Lifetime, builder.Configuration);

app.MapControllers();

await app.RunAsync();




