using urun.Models;
using Microsoft.EntityFrameworkCore;

namespace urun.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
       

    }
}
