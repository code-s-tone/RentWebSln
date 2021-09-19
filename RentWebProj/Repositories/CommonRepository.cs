using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RentWebProj.Models;

namespace RentWebProj.Repositories
{
    public class CommonRepository
    {
        private readonly RentContext _context;
        public CommonRepository()
        {
            _context = new RentContext();
        }

        public void Create<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void Update<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Modified;
        }
        public void Delete<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Deleted;
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }
        //.OrderBy(keySelector)
        //.FirstOrDefault(x => x.PartNo == partNo) 讓外面決定
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}