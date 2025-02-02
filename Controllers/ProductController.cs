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

        [Authorize]
        [HttpGet("search-parameters")]
        public IActionResult GetSearchParameters()
        {
            var searchParameters = new List<object>
            {
                new { Parameter = "product_type", DataType = "string", Description = "The type of makeup being searched for (e.g., lipstick, eyeliner)" },
                new { Parameter = "product_category", DataType = "string", Description = "Sub-category for each makeup-type (e.g., lip gloss is a category of lipstick)" },
                new { Parameter = "product_tags", DataType = "string, list separated by commas", Description = "Options each product could be tagged with (e.g., vegan)" },
                new { Parameter = "brand", DataType = "string", Description = "Brand of the product" },
                new { Parameter = "price_greater_than", DataType = "number", Description = "Will return a list of products with price greater than the indicated number (exclusive)" },
                new { Parameter = "price_less_than", DataType = "number", Description = "Will return a list of products with price less than the indicated number (exclusive)" },
                new { Parameter = "rating_greater_than", DataType = "number", Description = "Will return a list of products with a rating more than the indicated number (exclusive)" },
                new { Parameter = "rating_less_than", DataType = "number", Description = "Will return a list of products with a rating less than the indicated number (exclusive)" }
            };

            return Ok(searchParameters);
        }

        [Authorize]
        [HttpGet("taglist")]
        public IActionResult GetTagList()
        {
            var tagList = new List<string>
            {
                "Canadian",
                "CertClean",
                "Chemical Free",
                "Dairy Free",
                "EWG Verified",
                "EcoCert",
                "Fair Trade",
                "Gluten Free",
                "Hypoallergenic",
                "Natural",
                "No Talc",
                "Non-GMO",
                "Organic",
                "Peanut Free Product",
                "Sugar Free",
                "USDA Organic",
                "Vegan",
                "Alcohol Free",
                "Cruelty Free",
                "Oil Free",
                "Purpicks",
                "Silicone Free",
                "Water Free"
            };

            return Ok(tagList);
        }

        [Authorize]
        [HttpGet("brandlist")]
        public IActionResult GetBrandList()
        {
            var brandList = new List<string>
            {
                "almay", "alva", "anna sui", "annabelle", "benefit", "boosh", "burt's bees",
                "butter london", "c'est moi", "cargo cosmetics", "china glaze", "clinique",
                "coastal classic creation", "colourpop", "covergirl", "dalish", "deciem", "dior",
                "dr. hauschka", "e.l.f.", "essie", "fenty", "glossier", "green people", "iman",
                "l'oreal", "lotus cosmetics usa", "maia's mineral galaxy", "marcelle", "marienatie",
                "maybelline", "milani", "mineral fusion", "misa", "mistura", "moov", "nudus", "nyx",
                "orly", "pacifica", "penny lane organics", "physicians formula", "piggy paint",
                "pure anada", "rejuva minerals", "revlon", "sally b's skin yummies", "salon perfect",
                "sante", "sinful colours", "smashbox", "stila", "suncoat", "w3llpeople", "wet n wild",
                "zorah", "zorah biocosmetiques"
            };

            return Ok(brandList);
        }

    }
}
