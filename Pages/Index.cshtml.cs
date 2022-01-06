using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _memoryCache;
        public string CurrentDateTime;
        private const string CACHE_KEY = "CurrentTime";
        public IndexModel(IMemoryCache memoryCache, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
           // CurrentDateTime = DateTime.Now;

            if (!_memoryCache.TryGetValue(CACHE_KEY, out DateTime cacheValue))
            {
                cacheValue = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(240));

                _memoryCache.Set(CACHE_KEY, cacheValue, cacheEntryOptions);
                CurrentDateTime = "Not warmed up!";
                //Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            else
            {
                CurrentDateTime = "Warmed up at " + cacheValue.ToString();
            }
            //CurrentDateTime = cacheValue.ToString();
        }
    }
}
