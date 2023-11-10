namespace Web.API.Extensions.HttpClient;


public static class BaseHttpClientsRegistration
{

    public static IServiceCollection ConfigureHttpClientsApp(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<HttpClientDelegatingHandler>();


        services.AddHttpClient("router", c =>
        {
            c.BaseAddress = new Uri(configuration["urls:router"]);
        })
        .AddHttpMessageHandler<HttpClientDelegatingHandler>();

        services.AddHttpClient("identity", c =>
        {
            c.BaseAddress = new Uri(configuration["urls:identity"]);
        })
        .AddHttpMessageHandler<HttpClientDelegatingHandler>();

        services.AddHttpClient("ticket", c =>
        {
            c.BaseAddress = new Uri(configuration["urls:ticket"]);
        })
        .AddHttpMessageHandler<HttpClientDelegatingHandler>();


        // ============= Used API Gateway url is default for HttpClient =============
        services.AddTransient(sp =>
        {
            var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
            return clientFactory.CreateClient(configuration["urls:router"]);
        });

        return services;
    }



}