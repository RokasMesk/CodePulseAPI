using CodePulseAPI.Models.Domain;

namespace CodePulseAPI.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>>GetAllAsync();
    }
}
