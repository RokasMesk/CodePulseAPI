using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogPostRepository(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _db.BlogPosts.AddAsync(blogPost);
            await _db.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _db.BlogPosts.ToListAsync();
        }
    }
}
