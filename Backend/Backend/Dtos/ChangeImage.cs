using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Backend.Dtos
{
    public class ChangeImage
    {
       
        public IFormFile Image { get; set; }
    }
}
