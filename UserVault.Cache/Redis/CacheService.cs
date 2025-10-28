using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using UserVault.Application.Helper;
using UserVault.Application.Interfaces;

namespace UserVault.Cache.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly IServer _server;
        private readonly bool _redisCacheEnbled;
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, ICacheEntry> _cacheEntries = new();

        public CacheService(IMemoryCache memoryCache)
        {
            _redisCacheEnbled = CacheConnection.RedisCacheEnbled;
            if (_redisCacheEnbled)
            {
                _db = CacheConnection.Connection.GetDatabase();
                EndPoint[] endpoints = CacheConnection.Connection.GetEndPoints(configuredOnly: true);
                foreach (EndPoint endpoint in endpoints)
                {
                    _server = CacheConnection.Connection.GetServer(endpoint);
                }
            }
            else
            {
                _memoryCache = memoryCache;
            }
        }

        public bool Set<T>(string key, T value, DateTimeOffset options)
        {
            if (_redisCacheEnbled)
            {
                TimeSpan expiryTime = options.DateTime.Subtract(DateTime.Now);
                return _db.StringSet(key: key, value: JsonConvert.SerializeObject(value), expiry: expiryTime);
            }
            else
            {
                using ICacheEntry entry = _memoryCache.CreateEntry(key);
                entry.RegisterPostEvictionCallback(PostEvictionCallback);
                _cacheEntries.AddOrUpdate(key: key, addValue: entry, (o, cacheEntry) =>
                {
                    cacheEntry.Value = entry;
                    return cacheEntry;
                });
                entry.AbsoluteExpiration = options;
                entry.Value = value;

                return true;
            }
        }
        private void PostEvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            if (_redisCacheEnbled == false && reason != EvictionReason.Replaced)
                _cacheEntries.TryRemove(key: key.ToString(), value: out _);
        }
        public bool TryGetValue<T>(string key, out T value)
        {
            if (_redisCacheEnbled)
            {
                RedisValue keValue = _db.StringGet(key);
                if (string.IsNullOrEmpty(keValue))
                {
                    value = default;
                    return false;
                }

                value = JsonConvert.DeserializeObject<T>(keValue);
                return true;
            }
            else
            {
                return _memoryCache.TryGetValue(key: key, value: out value);
            }
        }
        public void Clear(string pattern)
        {
            if (_redisCacheEnbled)
            {
                foreach (RedisKey key in _server.Keys(pattern: $"*{pattern}*"))
                {
                    _db.KeyDelete(key: key);
                }
            }
            else
            {
                IEnumerable<string> keys = _cacheEntries.Keys.Where(key => key.IndexOf(pattern) != -1);
                foreach (string key in keys)
                {
                    _memoryCache.Remove(key: key);
                }
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _cacheEntries.Select(pair => new KeyValuePair<string, object>(pair.Key, pair.Value.Value)).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Dispose()
        {
            if (_redisCacheEnbled == false)
                _memoryCache.Dispose();
        }
    }
}
