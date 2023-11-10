using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Web.API.Models;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {

        private readonly ILogger<AppController> _logger;
        private readonly HttpClient _httpClient;


        private IHttpClientFactory _httpClientFactory;
        private HttpClient _identityClient;
        private HttpClient _ticketClient;


        public AppController(ILogger<AppController> logger, HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;

            _identityClient = httpClientFactory.CreateClient("identity");
            _ticketClient = httpClientFactory.CreateClient("ticket");

        }


        [HttpPost("[action]")]
        public async Task<ActionResult> GetToken([FromBody] AccountRequestModel model)
        {
            var content_str = JsonConvert.SerializeObject(model);
            var body_str = new StringContent(content_str, Encoding.UTF8, "application/json");

            var url = "/account/gettoken";
            var response = await _identityClient.PostAsync(url, body_str);

            // ================ 200 Başarı Dönüşler ================
            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());


        }




        [HttpGet("[action]")]
        public async Task<ActionResult> AvailableTickets([FromQuery] string token)
        {

            _ticketClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = "/ticket/tickets";
            var response = await _ticketClient.GetAsync(url);

            // ================ 200 Başarı Dönüşler ================
            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> BuyTicket([FromBody] TicketRequestModel model)
        {
            _ticketClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

            var content_str = JsonConvert.SerializeObject(model);
            var body_str = new StringContent(content_str, Encoding.UTF8, "application/json");

            var url = "/ticket/buyticket";
            var response = await _ticketClient.PostAsync(url, body_str);

            // ================ 200 Başarı Dönüşler ================
            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsStringAsync());

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}