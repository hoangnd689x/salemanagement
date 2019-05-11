using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Code.Caching
{
    public interface IApiCache
    {
        List<string> GetKeys(string pattern = "*");
        T Get<T>(string key) where T : class;
        void Set(string key, object obj);
        void Set(string key, object obj, int minutes);
        void RemoveAll(string pattern = "*");
        void Remove(string key);
    }
    public class ApiCache : IApiCache
    {

        private IMemoryCache _cache;

        public ApiCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public List<string> GetKeys(string pattern = "*")
        {
            return new List<string>();
        }

        public T Get<T>(string key) where T : class
        {
            return _cache.Get<T>(key);
        }

        public void Set(string key, object obj)
        {

            _cache.Set(key, obj);
        }
        public void Set(string key, object obj, int minutes)
        {
            _cache.Set(key, obj, TimeSpan.FromMinutes(minutes));
        }
        public void RemoveAll(string pattern = "*")
        {
            //_cache.key()
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }



    }

}
