using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Interfaces
{
    public interface IRedisRepository
    {
        /// <summary>
        /// 將資料存入MemoryCache(Redis)，對應資料庫Create、Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set<T>(string key, T value) where T : class;

        /// <summary>
        /// 取得MemoryCache(Redis)，對應資料庫Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// 移除MemoryCache(Redis)，對應資料庫Delete
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
