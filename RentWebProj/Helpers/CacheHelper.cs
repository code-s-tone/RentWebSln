using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using RentWebProj.ViewModels;
using RentWebProj.Repositories;
using RentWebProj.Services;

namespace RentWebProj.Helpers
{
    public static class CacheHelper
    {
        private static MemoryCache _cache = MemoryCache.Default;

        /// <summary>
        ///  TableData
        /// </summary>
        public static List<CardsViewModel> CardData
        {
            get
            {
                if (!_cache.Contains("TableData"))

                    RefreshTableData();
                //不存在就加入
                return _cache.Get("TableData") as List<CardsViewModel>;
            }
        }

        /// <summary>
        /// 更新 TableData
        /// </summary>
        public static void RefreshTableData()
        {
            //移除 cache 中資料
            _cache.Remove("TableData");

            //存取資料
            var listAgency = "";// new ProductService().GetCheapestProductCardData();

            //設定 cache 過期時間
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(9) };
            //加入 cache
            _cache.Add("TableData", listAgency, cacheItemPolicy);
        }
    }
}