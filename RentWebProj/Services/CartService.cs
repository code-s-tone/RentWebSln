using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;
using RentWebProj.ViewModels;
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

        public OperationResult Create(ProductDetailToCart VM, string PID)
        {
            var result = new OperationResult();
            try//寫入result
            {
                //資料庫若有防呆，不用檢查重複
                DateTime ExpirationDate = DateTime.Parse(VM.ExpirationDate);
                DateTime StartDate = Convert.ToDateTime(VM.StartDate);

                //VM->DM
                Cart entity = new Cart()
                {
                    MemberID = (int)VM.CurrentMemberID,
                    ProductID = PID,
                    StartDate = Convert.ToDateTime(VM.StartDate),
                    ExpirationDate = DateTime.Parse(VM.ExpirationDate)
                    //如何指定格式?
                };

                //判斷是否本來就存在
                if (VM.isExisted)
                {//更新
                    _repository.Update(entity);//猜測會用PK去找到原有的資料
                }
                else
                {//加入
                    _repository.Create(entity);
                }
                _repository.SaveChanges();

                //Bill教的
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {   //Bill教的
                result.IsSuccessful = false;
                result.Exception = ex;
            }

            return result;
        }

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
            //foreach (var item in CartIndex)
            //{
            //    item.Sub = item.DailyRate * item.DateDiff;
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
    }

}