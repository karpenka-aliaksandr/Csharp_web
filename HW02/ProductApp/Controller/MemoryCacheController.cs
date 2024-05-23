using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ProductApp.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MemoryCacheController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet(template: "getcachestats")]
        public ActionResult<MemoryCacheStatistics> GetCacheStats()
        {
            return _cache.GetCurrentStatistics();
        }
    }
}
