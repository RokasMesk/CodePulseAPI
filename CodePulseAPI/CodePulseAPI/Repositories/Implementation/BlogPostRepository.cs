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
            return await _db.BlogPosts.Include(x=> x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
           return await _db.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _db.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost == null)
            {
                return null;
            }
            _db.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.Categories = blogPost.Categories;
            await _db.SaveChangesAsync();
            return blogPost;
        }
        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost != null)
            {
                 _db.BlogPosts.Remove(existingBlogPost);
                await _db.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }

        public async Task<BlogPost?> GetBlogByUrlHandleAsync(string urlHandle)
        {
            return await _db.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }
    }
}
