using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{
    public class OrderService : IOrderService
    {
        private readonly RentContext _ctx;
        public OrderService(RentContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<OrderViewModel> GetOrderData()
        {
            var result = from o in _ctx.Orders
                         join m in _ctx.Members
                         on o.MemberId equals m.MemberId
                         join b in _ctx.BranchStores
                         on o.StoreId equals b.StoreId
                         join od in _ctx.OrderDetails
                         on o.OrderId equals od.OrderId
                         join p in _ctx.Products
                         on od.ProductId equals p.ProductId
                         select new OrderViewModel
                         {
                             OrderID = o.OrderId,
                             MemberID = o.MemberId,
                             FullName = m.FullName,
                             StoreName = b.StoreName,
                             Phone = m.Phone,
                             Email = m.Email,
                             OrderStatusID = o.OrderStatusId == 0 ? "已作廢" : o.OrderStatusId == 1 ? "待付款" : o.OrderStatusId == 2 ? "付款中" : o.OrderStatusId == 3 ? "付款中" : "沒有狀態",
                             GoodsStatusID = od.GoodsStatus == 0 ? "已歸還" : od.GoodsStatus == 1 ? "待出貨" : od.GoodsStatus == 2 ? "已出貨" : od.GoodsStatus == 3 ? "已到貨" : od.GoodsStatus == 4 ? "已取貨" : "沒有狀態",
                             OrderDate = o.OrderDate,
                             ProductName = p.ProductName,
                             DailyRate = od.DailyRate,
                             StartDate = od.StartDate,
                             ExpirationDate = od.ExpirationDate,
                             TotalAmount = od.TotalAmount
                             
                         };
            return result;
        }
    }
    //public int OrderID { get; set; }
    //public int MemberID { get; set; }
    //public string FullName { get; set; }
    //public string StoreName { get; set; }
    //public int Phone { get; set; }
    //public string Email { get; set; }
    //public int OrderStatusID { get; set; }
    //public int GoodsStatusID { get; set; }
    //public DateTime OrderDate { get; set; }

    //public string ProductName { get; set; }
    //public decimal DailyRate { get; set; }
    //public DateTime StartDate { get; set; }
    //public DateTime ExpirationDate { get; set; }
    //public decimal TotalAmount { get; set; }
}
