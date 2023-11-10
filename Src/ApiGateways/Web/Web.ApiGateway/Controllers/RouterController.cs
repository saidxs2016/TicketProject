using Microsoft.AspNetCore.Mvc;
using Ocelot.Requester;
using System.Net.Http;

namespace Web.ApiGateway.Controllers;

// ============ if i use Http Aggregation ============
[ApiController]
[Route("/v1/[controller]")]
public class RouterController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;  
    
    //private readonly HttpClient identity_client;
    //private readonly HttpClient ticket_client;
    //private readonly HttpClient payment_client;


    public RouterController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        //identity_client = _httpClientFactory.CreateClient("identity");
        //ticket_client = _httpClientFactory.CreateClient("ticket");
        //payment_client = _httpClientFactory.CreateClient("payment");
    }

   

}