using Grams.Server.Services.ImageService;
using Microsoft.AspNetCore.Mvc;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Image>>>> GetImages()
    {
        var response = await _imageService.GetImages();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Image>>> UploadImage([FromForm] IFormFile file)
    {
        var response = await _imageService.AddImage(file);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }


}
