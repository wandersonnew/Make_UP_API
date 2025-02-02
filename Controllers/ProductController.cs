using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProductController> _logger;
        private readonly IMemoryCache _memoryCache;
        private string url = "http://makeup-api.herokuapp.com/api/v1/products.json";

        public ProductController(HttpClient client, ILogger<ProductController> logger, IMemoryCache memoryCache)
        {
            _client = client;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var cacheKey = "makeup-products";
            var cachedProducts = _memoryCache.Get<string>(cacheKey);

            if (cachedProducts != null)
            {
                return Ok(cachedProducts); // Retorna os dados do cache
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync(this.url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Armazena os produtos no cache por 10 minutos
                _memoryCache.Set(cacheKey, responseBody, TimeSpan.FromMinutes(10));

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
