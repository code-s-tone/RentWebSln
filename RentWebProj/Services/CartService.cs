using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using System.Data.Entity.Core.Objects;

namespace RentWebProj.Services
{
    public class CartService
    {
        private CommonRepository _repository;
        public CartService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public OperationResult CreateOrUpdate(ProductDetailToCart VM , string PID)
        {//再判斷訂單卡時段?            
            var result = new OperationResult();
            try
            {
                //VM->DM
                Cart entity = new Cart()
                {
                    MemberID = 1,
                    ProductID = PID,
                    StartDate = Convert.ToDateTime(VM.StartDate),//空字串能否轉?
                    ExpirationDate = DateTime.Parse(VM.ExpirationDate)                    
                };
                //判斷是否本來就存在
                if ( VM.IsExisted )
                {//更新
                    _repository.Update(entity);//猜測會用PK去找到原有的資料
                }
                else
                {//加入
                    _repository.Create(entity);
                }
                _repository.SaveChanges();

                
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }

            return result;
        }
        //public IEnumerable<ProductCartsView> CheckCart(OrderDoubleCheck VM)
        //{
        //    IEnumerable<ProductCartsView> CCList;
        //    CCList = from o in _repository.GetAll<Order>()
        //             join p in _repository.GetAll<Product>() on 
                     


        //    return CCList;
        //}




        public IEnumerable<CartIndex> GetCart(int MemberID)
        {
            IEnumerable<CartIndex> CartIndex;

            var Member = _repository.GetAll<Member>();
            var Product = _repository.GetAll<Product>();
            var Cart = _repository.GetAll<Cart>();

            CartIndex = from c in Cart
                        join m in Member on c.MemberID equals m.MemberID
                        join p in Product on c.ProductID equals p.ProductID
                        where m.MemberID == MemberID
                        select new CartIndex
                        {
                            MemberID = c.MemberID,
                            ProductID = c.ProductID,
                            ProductName = p.ProductName,
                            StartDate = (DateTime)c.StartDate,
                            ExpirationDate = (DateTime)c.ExpirationDate,
                            DailyRate = (decimal)p.DailyRate,
                            Qty = 1,
                            Available = (bool)p.Available,
                            DateDiff = (int)EntityFunctions.DiffDays((DateTime)c.StartDate, (DateTime)c.ExpirationDate),
                            Sub = (decimal)p.DailyRate * ((int)EntityFunctions.DiffDays((DateTime)c.StartDate, (DateTime)c.ExpirationDate))
                        };

            var odSV = new OrderService();//軒
            //軒：每筆產品加入禁租日期，用select和foreach都失敗，所以才用這麼繞的方法
            var temp = CartIndex.ToList();
            temp.ForEach(c =>
                c.DisablePeriodsJSON = odSV.GetDisablePeriodJSON(c.ProductID)
            );
            CartIndex = temp.AsEnumerable();

            //CartIndex.Select(c =>
            //{
            //    CartIndex x = c;
            //    x.DisablePeriodsJSON = odSV.GetDisablePeriodJSON(c.ProductID);
            //    return x;
            //});

            //foreach (var item in CartIndex)
            //{
            //    //item.Sub = item.DailyRate * item.DateDiff;
            //}

            return CartIndex;
        }

        public decimal GetCartTotal(int MemberID)
        {
            var CartIndex = GetCart(MemberID);

            decimal CartTotal = 0;

            foreach (var item in CartIndex)
            {
                CartTotal = CartTotal + item.Sub;
            }

            return CartTotal;
        }

        public void DeleteCart(int MemberID, string ProductID)
        {
            Cart deleteList = new Cart() 
            {
                MemberID = MemberID,
                ProductID = ProductID
            };

            _repository.Delete<Cart>(deleteList);
            _repository.SaveChanges();
            int i = 0;
        }
    }
}