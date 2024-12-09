using urun.Models;

namespace urun.Repositories
{
    public class ContactRepository : GenericRepository<Contact>
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
} 