using System.Diagnostics;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Microsoft.Extensions.Logging;

namespace Starships.API.Filters
{
    public class CacheFilter : TypeFilterAttribute
    {
        public CacheFilter() : base(typeof(CacheFilterImpl)) { }

        class CacheFilterImpl : IAsyncActionFilter
        { 
            private readonly IDistributedCache _cache;
            private readonly Stopwatch _stopWatch = new Stopwatch();
            private readonly ILogger _logger;

            public CacheFilterImpl(
                    IDistributedCache cache, 
                    ILogger<CacheFilterImpl> logger)
            {
                _cache = cache;
                _logger = logger;
            }

            public async Task OnActionExecutionAsync(
                ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                _stopWatch.Reset();
                _stopWatch.Start();
                var cacheKey = GetCacheKey(context);

                Console.WriteLine($"cache key - {cacheKey}");

                var cachedString = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedString)) 
                {
                    _stopWatch.Stop();
                    _logger.LogInformation("retrieved starships from cache in {}ms ", _stopWatch.ElapsedMilliseconds.ToString());
                    var cachedObj =  JsonConvert.DeserializeObject(cachedString);
                    context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult(cachedObj);
                } 
                else 
                {
                    var resultContext = await next();
                    
                    var result = resultContext.Result;
                    if (result != null)
                    {
                        var value = result.GetType().GetProperty("Value").GetValue(result, null);

                        if (value != null) 
                        {
                            await SetCache(cacheKey, value);
                        }
                    }
                    _stopWatch.Stop();
                    _logger.LogInformation("retrieved starships from remote api in {}ms ", _stopWatch.ElapsedMilliseconds.ToString());
                }
            }

            private async Task SetCache(string cacheKey, object value)
            {
                var cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                var jsonSerializerSettings = new JsonSerializerSettings();
                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                var jsonString = JsonConvert.SerializeObject(value, jsonSerializerSettings);
                await _cache.SetStringAsync(cacheKey, jsonString, cacheOptions);   
            }

            private string GetCacheKey(ActionExecutingContext context)
            {   
                var cacheKey = context.HttpContext.Request.Path.Value.Replace("/", "-");
                return cacheKey;
            }
        }
    }
}
