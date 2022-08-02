using Library.Core.CrossCuttingConcerns.Caching;
using Library.Core.DependencyResolvers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.MicrosoftMemoryCache
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceHelper.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _ );
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache)
                .GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(this._memoryCache) as dynamic;
            var cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheCollectionItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheCollectionItem.GetType()
                    .GetProperty("Value").GetValue(cacheCollectionItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }  

            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var keysToRemove = cacheCollectionValues.Where(c => regex.IsMatch(c.Key.ToString()))
                .Select(c => c.Key).ToList();
            foreach (var keyToRemove in keysToRemove)
            {
                _memoryCache.Remove(keyToRemove);
            }
        }
    }
}
