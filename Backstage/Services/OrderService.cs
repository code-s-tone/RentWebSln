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
                             TotalAmount = od.TotalAmount

                         };
            return result;
        }

        public Task<IEnumerable<OrderViewModel>> UpdateOrder(OrderViewModel UpdateOrder)
        {
            var order = _ctx.OrderDetails.Where(x => x.OrderId == UpdateOrder.OrderID).FirstOrDefault();
            var orderQuery = (
                    from od in _ctx.OrderDetails
                    where od.OrderId == UpdateOrder.OrderID
                    join o in _ctx.Orders on od.OrderId equals o.OrderId
                    join m in _ctx.Members on o.MemberId equals m.MemberId
                    select m
                ).FirstOrDefault();

            if (UpdateOrder.GoodsStatusID == "已歸還")
            {
                order.GoodsStatus = 0;
            }
            else if (UpdateOrder.GoodsStatusID == "待出貨")
            {
                order.GoodsStatus = 1;
            }
            else if (UpdateOrder.GoodsStatusID == "已出貨")
            {
                order.GoodsStatus = 2;
            }
            else if (UpdateOrder.GoodsStatusID == "已到貨")
            {
                order.GoodsStatus = 3;
            }
            else if (UpdateOrder.GoodsStatusID == "已取貨")
            {
                order.GoodsStatus = 4;
            }


            //_ctx.SaveChanges();
            return null;
        }

        public async Task<IEnumerable<ProductViewModel>> UpdateProduct(ProductViewModel UpdateProduct)
        {
            //不含圖的修改部分
            DateTime date1 = DateTime.Now;//修改當天的日期
            var product = _ctx.Products.Where(x => x.ProductId == UpdateProduct.ProductId).FirstOrDefault();
            product.ProductName = UpdateProduct.ProductName;
            product.Description = UpdateProduct.Description;
            product.DailyRate = UpdateProduct.DailyRate;
            product.LaunchDate = UpdateProduct.LaunchDate;
            product.WithdrawalDate = UpdateProduct.WithdrawalDate;
            product.Discontinuation = UpdateProduct.Discontinuation;
            product.UpdateTime = date1;
            //圖片刪除部分
            //1.這邊是先刪除資料庫的圖片
            var productImages = _ctx.ProductImages.Where(x => x.ProductId == UpdateProduct.ProductId).ToList();//先刪掉所有在資料庫的圖片
            _ctx.RemoveRange(productImages);//利用RemoveRange可以刪除集合的特性一次刪除
            _ctx.SaveChanges();
            //2.再來刪除圖庫的存檔地點
            var cloudImg = UpdateProduct.ProductImages.Where(x => !x.SourceImages.Contains("data")).ToList();//取的DataUrl的圖源的
            var cloudImgLength = cloudImg.Count;//取的有幾張圖
            var cloudName = UpdateProduct.ProductId;//取的public ID
            var CloudFolder = UpdateProduct.ProductId.Substring(0, 3);//取的在哪個大類資料夾
            //DeleteImg(CloudFolder, cloudName, cloudImgLength);
            //3.新增AllImg把所有圖片放進取(包含dataUrl)
            var AllImg = UpdateProduct.ProductImages.ToList();

            return null;
        }



    }
}
