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
                             TotalAmount = od.TotalAmount,
                             StoreID = o.StoreId
                             
                         };
            return result;
        }

        public IEnumerable<OrderViewModel> GetOrderDetailData(int orderID)
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
                         where o.OrderId == orderID
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
                             TotalAmount = od.TotalAmount,
                             StoreID = o.StoreId

                         };
            return result;
        }

        
        public EditOrderListResponseModel UpdateOrder(OrderViewModel UpdateOrder)
        {
            EditOrderListResponseModel result = new EditOrderListResponseModel()
            {
                Status = true,
                Message = string.Empty
            };

            try
            {
                //出貨狀態
                var OD = _ctx.OrderDetails.Where(x => x.OrderId == UpdateOrder.OrderID).FirstOrDefault();

                if (UpdateOrder.GoodsStatusID == "已歸還")
                {
                    OD.GoodsStatus = 0;
                }
                else if (UpdateOrder.GoodsStatusID == "待出貨")
                {
                    OD.GoodsStatus = 1;
                }
                else if (UpdateOrder.GoodsStatusID == "已出貨")
                {
                    OD.GoodsStatus = 2;
                }
                else if (UpdateOrder.GoodsStatusID == "已到貨")
                {
                    OD.GoodsStatus = 3;
                }
                else if (UpdateOrder.GoodsStatusID == "已取貨")
                {
                    OD.GoodsStatus = 4;
                }

                //姓名、電話
                var member = (
                        from od in _ctx.OrderDetails
                        where od.OrderId == UpdateOrder.OrderID
                        join o in _ctx.Orders on od.OrderId equals o.OrderId
                        join m in _ctx.Members on o.MemberId equals m.MemberId
                        select m
                    ).FirstOrDefault();
                member.FullName = UpdateOrder.FullName;
                member.Phone = UpdateOrder.Phone;

                //分店
                var order = _ctx.Orders.Where(x => x.OrderId == UpdateOrder.OrderID).FirstOrDefault();

                order.StoreId = UpdateOrder.StoreID;
                var a = 1;
                


                
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "EditOrderList Error !";  //ex.Message + ex.StackTrace;                
            }
            

            return result;
        }
    }
}
