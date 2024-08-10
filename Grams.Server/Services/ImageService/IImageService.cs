namespace Grams.Server.Services.ImageService;

public interface IImageService
{
    Task<ServiceResponse<List<Image>>> GetImages();
    Task<ServiceResponse<Image>> AddImage(IFormFile file);
}
