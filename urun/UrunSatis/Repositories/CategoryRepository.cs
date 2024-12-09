using urun.Models;
using Microsoft.EntityFrameworkCore;

namespace urun.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
} 