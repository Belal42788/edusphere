using Backend.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Backend.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public string SetImage(IFormFile imgFile)
        {
            var imgGuid = Guid.NewGuid();
            var imgExtension = Path.GetExtension(imgFile.FileName);
            var imgName = imgGuid + imgExtension;
            var imgUrl = "\\images\\" + imgName;
            var imgPath = _webHostEnvironment.WebRootPath + imgUrl;

            using (var imgStream = new FileStream(imgPath, FileMode.Create))
            {
                imgFile.CopyTo(imgStream);
            }

            return imgUrl;
        }
        public void DeleteImage(string imgUrl)
        {
            var imgOldPath = _webHostEnvironment.WebRootPath + imgUrl;

            if (File.Exists(imgOldPath))
            {
                File.Delete(imgOldPath);
            }
        }


        public string UpdateImage(IFormFile imgFile, string oldImageUrl)
        {
            DeleteImage(oldImageUrl);
            return SetImage(imgFile);
        }
    }
}