using Backstage.Interfaces;
using Backstage.Models;
using Backstage.ViewModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Services
{

    public class ProductService: IProductService
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

        public async Task<ApiResponse> UpdateProduct(ProductViewModel UpdateProduct)
        {
            //處理錯誤訊息
            var apipesponse = new ApiResponse();

            //不含圖的修改部分
            DateTime date1 = DateTime.Now;//修改當天的日期
            var product = _ctx.Products.Where(x => x.ProductId == UpdateProduct.ProductId).FirstOrDefault();//判斷編輯或新增與否

            if (UpdateProduct.NewProduct == false && product != null )//如果查詢後的資料不等於null和新建模式是False(NewProduct)=修改資料
            {
                try
                {
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
                    _ctx.SaveChanges();//存檔

                    //2.去圖庫創建檔案，以覆蓋圖庫的存檔地點
                    var cloudName = UpdateProduct.ProductId;//取的public ID
                    var CloudFolder = UpdateProduct.ProductId.Substring(0, 3);//取的在哪個大類資料夾
                    var AllImg = UpdateProduct.ProductImages.ToList(); //新增AllImg把所有圖片放進取(包含dataUrl)    
                    ;
                    //刪除同ProductID的圖片(最多十張所以設10)
                    DeleteImg(CloudFolder, cloudName, 10);
                    //新增圖庫圖片      
                    CreatImg(CloudFolder, cloudName, AllImg);
        
                    apipesponse.IsSuccessful = true;
                    apipesponse.Result = "編輯產品成功";
                    return apipesponse;
                }
                catch (Exception ex)
                {
                    apipesponse.IsSuccessful = false;
                    apipesponse.Result = "編輯產品失敗";
                    return apipesponse;
                }



            }
            else if (UpdateProduct.NewProduct == true && product == null  )//如果查詢後的資料等於null和新建模式是True(NewProduct)=新增資料
            {
                try
                {
                    var cloudName = UpdateProduct.ProductId;//取的public ID
                    var CloudFolder = UpdateProduct.ProductId.Substring(0, 3);//取的在哪個大類資料夾
                    var AllImg = UpdateProduct.ProductImages.ToList(); //新增AllImg把所有圖片放進取(包含dataUrl   
                    CreatImg(CloudFolder, cloudName, AllImg);
                    //新增資料的部分
                    _ctx.Add(new Product
                    {
                        ProductId = UpdateProduct.ProductId,
                        ProductName = UpdateProduct.ProductName,
                        Description = UpdateProduct.Description,
                        DailyRate = UpdateProduct.DailyRate,
                        LaunchDate = UpdateProduct.LaunchDate,
                        WithdrawalDate = UpdateProduct.WithdrawalDate,
                        UpdateTime = UpdateProduct.UpdateTime,
                        Discontinuation=UpdateProduct.Discontinuation
                        
                    });
                    _ctx.SaveChanges();//存檔
                    apipesponse.Result = "新增產品成功";
                    return apipesponse;
                }
                catch (Exception ex)
                {

                    apipesponse.Result = "新增產品失敗";
                    return apipesponse;
                }

            }
            else
            {

                apipesponse.Result = "重複產品名稱";
                return apipesponse;
            }



        }

        public void CreatImg(string CloudFolder, string ImgName, List<ImageViewModel> Allimg)
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
        //刪除雲端的照片
        public void DeleteImg(string CloudFolder, string ImgName, int QuantityImg)
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
                ;
            }
        }


    }
}
