using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Models;
using RentWebProj.ViewModels;
using RentWebProj.Repositories;

namespace RentWebProj.Services
{
    public class CartService
    {
        private CommonRepository _repository;
        public CartService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public OperationResult Create(ProductDetailToCart VM , string PID)
        {
            var result = new OperationResult();
            try//寫入result
            {
                //資料庫若有防呆，不用檢查重複
                //VM->DM
                DateTime ExpirationDate = DateTime.Parse(VM.ExpirationDate);
                DateTime StartDate = Convert.ToDateTime(VM.StartDate);

                Cart entity = new Cart()
                {
                    MemberID = (int)VM.CurrentMemberID,
                    ProductID = PID,
                    StartDate = Convert.ToDateTime(VM.StartDate),
                    ExpirationDate = DateTime.Parse(VM.ExpirationDate)
                    //如何指定格式?
                };

                //判斷是否本來就存在
                if ( VM.isExisted )
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
    }

}