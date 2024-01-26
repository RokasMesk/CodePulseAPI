using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        //
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            Category category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            await _categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }
        // GET: https://localhost:7250/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = new List<CategoryDTO>();
            // Map domain model to DTO
            foreach (var category in categories)
            {
                response.Add(new CategoryDTO
                {
                    Id=category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }

    }
}
