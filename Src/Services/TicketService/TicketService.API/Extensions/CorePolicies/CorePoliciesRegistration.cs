namespace TicketService.API.Extensions.CorePolicies;


public static class CorePoliciesRegistration
{

    public static IServiceCollection ConfigureCorePolicies(this IServiceCollection services)
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