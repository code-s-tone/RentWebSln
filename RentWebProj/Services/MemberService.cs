using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.Services
{
    public class MemberService
    {
        private CommonRepository _repository;
        public MemberService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public bool getMemberLogintData(string Email, string Password)
        {
            var pDMList = _repository.GetAll<Member>();
            var result = pDMList.Where(x => x.Email == Email && x.PasswordHash == Password).FirstOrDefault()==null ? false: true;
            return result;
        }
        public void getMemberRegistertData(string Email, string Password)
        {
            var pDMList = _repository.GetAll<Member>();
            var result = pDMList.Where(x => x.Email == Email && x.PasswordHash == Password).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = Email, PasswordHash = Password, SignWayID = 1 };
                var repository = new CommonRepository(new RentContext());
                repository.Create(entity);
                repository.SaveChanges();
            }
 
        }
    }
}