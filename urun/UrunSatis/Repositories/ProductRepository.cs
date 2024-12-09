using urun.Models;
using Microsoft.EntityFrameworkCore;

namespace urun.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetProductWithCategory(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
} 