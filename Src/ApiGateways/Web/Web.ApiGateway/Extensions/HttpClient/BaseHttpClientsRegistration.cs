namespace Web.ApiGateway.Extensions.HttpClient;


public static class BaseHttpClientsRegistration
{

    public static IServiceCollection ConfigureHttpClientsApp(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddTransient<HttpClientDelegatingHandler>();


        //services.AddHttpClient("idenity", c =>
        //{
        //    c.BaseAddress = new Uri(configuration["urls:identity"]);
        //})
        //.AddHttpMessageHandler<HttpClientDelegatingHandler>();

        //services.AddHttpClient("ticket", c =>
        //{
        //    c.BaseAddress = new Uri(configuration["urls:ticket"]);
        //})
        //.AddHttpMessageHandler<HttpClientDelegatingHandler>();

        //services.AddHttpClient("payment", c =>
        //{
        //    c.BaseAddress = new Uri(configuration["urls:payment"]);
        //})
        //.AddHttpMessageHandler<HttpClientDelegatingHandler>();


        return services;
    }



}