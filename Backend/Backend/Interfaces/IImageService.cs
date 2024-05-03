namespace Backend.Interfaces
{
    public interface IImageService
    {
        string SetImage(IFormFile imgFile);
        void DeleteImage(string imgUrl);
        string UpdateImage(IFormFile imgFile, string oldImageUrl);
    }
}
