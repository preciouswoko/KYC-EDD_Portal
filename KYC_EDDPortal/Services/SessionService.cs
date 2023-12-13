using KYC_EDDPortal.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class SessionService : ISessionService
    {
        private readonly IMemoryCache _cache;
        private static ILogger<AumsService> _logging;

        public SessionService(IMemoryCache cache, ILogger<AumsService> logging)
        {
            _cache = cache;
            _logging = logging;
        }

        public void Set<T>(string key, T value)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
                };

                _cache.Set(key, value, cacheEntryOptions);
            }
            catch(Exception ex)
            {
                _logging.LogError(ex.Message, "SessionService:Set");
            }
          
        }

        public T Get<T>(string key)
        {
            try
            {
                if (_cache.TryGetValue(key, out T value))
                {
                    return value;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message, "SessionService:Get");
            }
           

            return default;
        }
        public void Clear(string key)
        {
            _cache.Remove(key);
        }
    }
}

