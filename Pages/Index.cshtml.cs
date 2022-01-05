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
        public DateTime CurrentDateTime;
        private const string CACHE_KEY = "CurrentTime";
        public IndexModel(IMemoryCache memoryCache, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
            CurrentDateTime = DateTime.Now;

            if (!_memoryCache.TryGetValue(CACHE_KEY, out DateTime cacheValue))
            {
                cacheValue = CurrentDateTime;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(120));

                _memoryCache.Set(CACHE_KEY, cacheValue, cacheEntryOptions);
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }

            CurrentDateTime = cacheValue;
        }
    }
}
