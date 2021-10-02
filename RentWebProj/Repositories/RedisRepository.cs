﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;


namespace RentWebProj.Repositories
{
    public static class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> Connection;
        static RedisConnectionFactory()
        {
            var connStr = System.Configuration.ConfigurationManager.AppSettings["RedisConnection"];
            var options = ConfigurationOptions.Parse(connStr);
            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }
        public static ConnectionMultiplexer GetConnection() => Connection.Value;
    }

    public class RedisRepository: IRedisRepository
    {
        //readonly IDistributedCache _iDistributedCache;
        //public RedisRepository(IDistributedCache distributedCache)
        //{
        //    _iDistributedCache = distributedCache;
        //}
        private static ConnectionMultiplexer _conn;
        private static IDatabase _db;
        static RedisRepository()
        {
            _conn = RedisConnectionFactory.GetConnection();
            _db = _conn.GetDatabase();
        }
        public T Get<T>(string key) where T : class
        {
            //return ByteArrayToObject<T>(_iDistributedCache.Get(key));
            var a = _db.StringGet(key);
            return ByteArrayToObject<T>(a);
        }

        public void Set<T>(string key, T value) where T : class
        {
            //_iDistributedCache.Set(key, ObjectToByteArray(value), new DistributedCacheEntryOptions()
            //{
            //    //快取存續時間暫定9分鐘
            //    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(9)
            //});
            TimeSpan cacheItemPolicy = new TimeSpan(0, 0, 9, 0);
            _db.StringSet(key, ObjectToByteArray(value), cacheItemPolicy);

        }

        public void Remove(string key)
        {
            //_iDistributedCache.Remove(key);
            _db.KeyDelete(key);
        }


        //object序列化成byte陣列
        private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
            //var json = JsonConvert.SerializeObject(obj);
            //return Encoding.UTF8.GetBytes(json);
        }

        //回傳反序列化泛型物件
        private T ByteArrayToObject<T>(byte[] bytes) where T : class
        {
            return bytes is null ? null : JsonSerializer.Deserialize<T>(bytes);
            //var obj = JsonConvert.DeserializeObject(bytes);
            //return bytes is null ? null : obj;
            //return null;
        }
    }
}