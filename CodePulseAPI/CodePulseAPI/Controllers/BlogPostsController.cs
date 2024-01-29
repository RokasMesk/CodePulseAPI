using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepostiory;
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepostiory = blogPostRepository;
        }
        // POST: {apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                UrlHandle = request.UrlHandle
            };
            await _blogPostRepostiory.CreateAsync(blogPost);
            // Convert Domain model back to dto
            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                IsVisible = blogPost.IsVisible,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle
            };
            return Ok(response);
        }
        // GET: {apibaseurl}/api/blogposts
        [HttpGet]
        public async  Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepostiory.GetAllAsync();
            // convert domain model to DTO
            var response = new List<BlogPostDTO>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDTO
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    IsVisible = blogPost.IsVisible,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle
                });
            }
            return Ok(response);
        }
    }
}
