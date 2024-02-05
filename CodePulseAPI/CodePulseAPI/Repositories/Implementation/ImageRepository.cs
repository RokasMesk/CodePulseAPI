using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _db;
        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContext,
            ApplicationDbContext db) 
        { 
            this._webHostEnvironment = webHostEnvironment;
            this._httpContext = httpContext;
            this._db    = db;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await _db.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            // uploading image
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);
            // update the database
            var httpRequest = _httpContext.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;
            await _db.BlogImages.AddAsync(blogImage);
            await _db.SaveChangesAsync();
            return blogImage;
            
        }
    }
}
