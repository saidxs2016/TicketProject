using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Web.ApiGateway.Extensions;
using Web.ApiGateway.Extensions.Auth;
using Web.ApiGateway.Extensions.CorePolicies;
using Web.ApiGateway.Extensions.HttpClient;

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

// ============== Http Aggregation ==============
builder.Services.ConfigureHttpClientsApp(builder.Configuration);


builder.Services.AddOcelot().AddConsul();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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


// ============== Reforce use https ============== 
//app.UseHttpsRedirection();

// ============== enable routing for controller ==============
app.UseRouting();

// ============== allow Cors Policy ==============
app.UseCors("CorsPolicy");

// ============== Authentication & Authorization ==============
//app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

await app.UseOcelot();

await app.RunAsync();




