using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;
using RentWebProj.ViewModels;
using RentWebProj.Repositories;

namespace RentWebProj.Services
{
    public class IndexService
    {
        private CommonRepository _repository;
        public IndexService()
        {
            _repository = new CommonRepository(new RentContext());
        }
        //0個參考...
        public IEnumerable<ProductCartsView> getCartsData()
        {
            IEnumerable<ProductCartsView> CMList;

            CMList = (from p in (_repository.GetAll<Product>())
                      join o in (_repository.GetAll<OrderDetail>())
                      on p.ProductID equals o.ProductID
                      select new ProductCartsView
                      { ProductName = p.ProductName }
            );

            return CMList;
        }

    }
}