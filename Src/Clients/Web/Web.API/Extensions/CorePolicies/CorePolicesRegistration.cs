namespace Web.API.Extensions.CorePolicies;


public static class CorePoliciesRegistration
{

    public static IServiceCollection ConfigureCorePolices(this IServiceCollection services)
    {

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }    

}