using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{

    public class ProductService : IProductService
    {
        readonly RentContext _ctx;
        public ProductService(RentContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<ProductViewModel> GetProduct(string ProductId)
        {

            if (!string.IsNullOrEmpty(ProductId))
            {
                IEnumerable<ProductViewModel> result =
                        from p in _ctx.Products
                        where p.ProductId == ProductId
                        let resutk = (from pi in _ctx.ProductImages
                                      where pi.ProductId == ProductId
                                      select new ImageViewModel { SourceImages = pi.Source }).ToList()
                        select new ProductViewModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            DailyRate = p.DailyRate,
                            Description = p.Description,
                            Discontinuation = p.Discontinuation,
                            LaunchDate = p.LaunchDate,
                            WithdrawalDate = p.WithdrawalDate,
                            UpdateTime = p.UpdateTime,
                            ProductImages = resutk
                        };
                return result;
            }

            else
            {
                IEnumerable<ProductViewModel> result =
                  from p in _ctx.Products

                  let resutk = (from pi in _ctx.ProductImages
                                where pi.ProductId == p.ProductId
                                select new ImageViewModel { SourceImages = pi.Source }).ToList()
                  select new ProductViewModel
                  {
                      ProductId = p.ProductId,
                      ProductName = p.ProductName,
                      DailyRate = p.DailyRate,
                      Description = p.Description,
                      Discontinuation = p.Discontinuation,
                      LaunchDate = p.LaunchDate,
                      WithdrawalDate = p.WithdrawalDate,
                      UpdateTime = p.UpdateTime,
                      ProductImages = resutk
                  };
                return result;
            }

        }

        public async  Task<IEnumerable<ProductViewModel>> UpdateProduct(ProductViewModel UpdateProduct)
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
            CreatImg(CloudFolder, cloudName, AllImg);
            
           return null;
        }

        public void CreatImg(string CloudFolder, string ImgName,List<ImageViewModel>Allimg)
        {
            try
            {
                Account account = new Account(
                  "dgaodzamk",
                  "192222538187587",
                  "OG8h1MXpd4lG1N0blyuNA4lETsQ");

                Cloudinary cloudinary = new Cloudinary(account);

                for (int i = 0; i < Allimg.Count; i++)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Allimg[i].SourceImages),//檔案來源
                        PublicId = $"Product/{CloudFolder}/{ImgName}_{i + 1}"//目標路徑
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);  //上傳

                    _ctx.Add(new ProductImage { ProductId = ImgName, ImageId = i + 1, Source = uploadResult.SecureUrl.ToString() }); //利用上傳成功的callbal組成ProductImga的資料型態


                }
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                ;
            }


        }
        public void DeleteImg(string CloudFolder, string ImgName,int QuantityImg)
        {
            Account account = new Account(
              "dgaodzamk",
              "192222538187587",
              "OG8h1MXpd4lG1N0blyuNA4lETsQ");
            Cloudinary cloudinary = new Cloudinary(account);
            for (int i = 1; i <= QuantityImg; i++)
            {
                var result = $"Product/{CloudFolder}/{ImgName}_{i}";
                var uploadResult = cloudinary.DeleteResources(result);  //刪除
                

            }


        }
    }
}
