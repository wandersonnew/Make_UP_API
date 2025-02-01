using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProductController> _logger;

        public ProductController(HttpClient client, ILogger<ProductController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            string url = "http://makeup-api.herokuapp.com/api/v1/products.json";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Erro ao obter produtos: {e.Message}");
                return StatusCode(500, "Erro ao obter produtos da API");
            }
        }
    }
}
