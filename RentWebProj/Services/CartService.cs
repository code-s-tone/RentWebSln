using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Repositories;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace RentWebProj.Services
{
    public class CartService
    {
        private CommonRepository _repository;
        public CartService()
        {
            _repository = new CommonRepository();
        }
        //產品列表頁入車，兩種可能
        public OperationResult Create(string PID)
        {
            var result = new OperationResult();
            try
            {
                //VM->DM
                Cart entity = new Cart()
                {
                    MemberID = (int)Helper.GetMemberId(),
                    ProductID = PID,
                };
                _repository.Create(entity);
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
        //產品細節頁入車，兩種可能
        public OperationResult CreateOrUpdate(ProductDetailToCart VM , string PID)
        {           
            var result = new OperationResult();
            try
            {
                DateTime? s = Convert.ToDateTime(VM.StartDate);
                DateTime? e = Convert.ToDateTime(VM.ExpirationDate);
                if (s == DateTime.MinValue) s = null;
                if (e == DateTime.MinValue) e = null;

                //VM->DM
                Cart entity = new Cart()
                {
                    MemberID = (int)Helper.GetMemberId(),
                    ProductID = PID,
                    StartDate = s,
                    ExpirationDate = e                    
                };
                //判斷是否本來就存在
                if (VM.IsExisted)
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

        public CartIndex CheckCart(string PID, int MemberID)
        {
            return GetCart(MemberID).SingleOrDefault(x => x.ProductID == PID);
        }

        public IEnumerable<CartIndex> GetCart(int MemberID)
        {
            IEnumerable<CartIndex> CartIndex;

            var Member = _repository.GetAll<Member>();
            var Product = _repository.GetAll<Product>();
            var Cart = _repository.GetAll<Cart>();
            var Img = _repository.GetAll<ProductImage>();

            CartIndex = from c in Cart
                        join m in Member on c.MemberID equals m.MemberID
                        join p in Product on c.ProductID equals p.ProductID
                        join i in Img on c.ProductID equals i.ProductID
                        where m.MemberID == MemberID && i.ImageID == 1
                        select new CartIndex
                        {
                            MemberID = c.MemberID,
                            ProductID = c.ProductID,
                            ProductName = p.ProductName,
                            StartDate = c.StartDate,
                            ExpirationDate = c.ExpirationDate,
                            DailyRate = p.DailyRate,
                            Qty = 1,
                            ImgSources = i.Source
                            //DateDiff = (int)DbFunctions.DiffDays((DateTime)c.StartDate, (DateTime)c.ExpirationDate),
                            //Sub = (decimal)p.DailyRate * ((int)DbFunctions.DiffDays((DateTime)c.StartDate, (DateTime)c.ExpirationDate))
                        };

            //軒：每筆產品加入禁租日期，用select和foreach都失敗，所以才用這麼繞的方法
            var odSV = new OrderService();
            var temp = CartIndex.ToList();
            temp.ForEach(c =>
            {
                int dateDiff = 0;
                if (c.StartDate.HasValue)
                {
                    dateDiff = (int)Math.Ceiling((c.ExpirationDate - c.StartDate).Value.TotalDays); //TotalDays帶小數
                }
                c.DateDiff = dateDiff;
                c.Sub = c.DailyRate * dateDiff;
                c.DisablePeriodsJSON = odSV.GetDisablePeriodJSON(c.ProductID);
            });

            //foreach (var item in CartIndex)
            //{
            //    //item.Sub = item.DailyRate * item.DateDiff;
            //}

            //CartIndex = temp.AsEnumerable();
            return temp;// CartIndex;
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
        }
  
        public void ECPayResponse(string MerchantTradeNo)
        {
            int TradeNo = Int32.Parse(MerchantTradeNo.Substring(5));
            var result = _repository.GetAll<Order>()
                .FirstOrDefault(x => x.OrderID == TradeNo);

            result.OrderStatusID = 3;

            _repository.SaveChanges();
        }
    }
}