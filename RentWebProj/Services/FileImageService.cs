using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RentWebProj.Models;
using RentWebProj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.Services
{
    public class FileImageService
    {
        private CommonRepository _repository;
        public FileImageService()
        {
            _repository = new CommonRepository(new RentContext());
        }


        public string HeadstickerImageData(string UserID)
        {
            var result = _repository.GetAll<ProductImage>();

            return result.ToList().Find(x => x.ProductID == UserID).Source;
        }
            public string FileImageData(string blobUrl)
        {

                Account account = new Account(
                  "dk3i5ui38",
                  "575296163116879",
                  "KPMZ4Q0Gae1KUKCmNS6hCgjBmpw");

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(blobUrl),
                    PublicId = "vivi"

                };
                var uploadResult = cloudinary.Upload(uploadParams);

                var getResultImgUrl = cloudinary.GetResource("vivi").SecureUrl;
                var result = _repository.GetAll<ProductImage>();
                result.ToList().Find(x => x.ProductID == "vivi").Source = getResultImgUrl; ;
                _repository.SaveChanges();
            
                return getResultImgUrl;
         }

    }
}