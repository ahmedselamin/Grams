namespace Grams.Server.Services.ImageService;

public class ImageService : IImageService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;

    public ImageService(DataContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    public async Task<ServiceResponse<List<Image>>> GetImages()
    {
        var response = new ServiceResponse<List<Image>>();
        try
        {
            response.Data = await _context.Images
                .OrderByDescending(img => img.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<Image>> AddImage(IFormFile file)
    {
        var response = new ServiceResponse<Image>();

        try
        {
            if (file != null && file.Length > 0)
            {
                //ensure uploads folder exists
                var uploadDir = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                //define filepath and save file
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var image = new Image { FileName = file.FileName, FilePath = filePath };
                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                response.Data = image;
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
