using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Backstage.Interfaces;

namespace Backstage.Repositories
{
    public class RedisRepository:IRedisRepository
    {
        readonly IDistributedCache _iDistributedCache;

        public RedisRepository(IDistributedCache distributedCache)
        {
            _iDistributedCache = distributedCache;
        }
        
        public void Set<T>(string key, T value) where T : class
        {
            _iDistributedCache.Set(key, ObjectToByteArray(value), new DistributedCacheEntryOptions()
            {
                //快取存續時間暫定9分鐘
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(9)
            });
        }

        public T Get<T>(string key) where T : class
        {
            return ByteArrayToObject<T>(_iDistributedCache.Get(key));
        }

        public void Remove(string key)
        {
            _iDistributedCache.Remove(key);
        }


        //object序列化成byte陣列
        private byte[] ObjectToByteArray(object obj)
        {
            //using System.Text.Json;
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        //回傳反序列化泛型物件
        private T ByteArrayToObject<T>(byte[] bytes) where T : class
        {
            return bytes is null ? null : JsonSerializer.Deserialize<T>(bytes);
        }
    }
}
