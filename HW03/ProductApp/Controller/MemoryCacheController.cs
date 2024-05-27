using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

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


        [HttpGet(template: "getcachestatsurl")]
        public ActionResult<String> GetCacheStatsUrl()
        {
            var stats = _cache.GetCurrentStatistics();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CurrentEntryCount;" + stats?.CurrentEntryCount.ToString());
            sb.AppendLine("CurrentEstimatedSize;" + stats?.CurrentEstimatedSize.ToString());
            sb.AppendLine("TotalMisses;" + stats?.TotalMisses.ToString());
            sb.AppendLine("TotalHits;" + stats?.TotalHits.ToString());
            var content = sb.ToString();

            string fileName = null;
            fileName = "MemoryCacheStats" + DateTime.Now.ToBinary().ToString() + ".csv";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);

            return "https://" + Request.Host.ToString() + "/static/" + fileName;
        }

    }
}
