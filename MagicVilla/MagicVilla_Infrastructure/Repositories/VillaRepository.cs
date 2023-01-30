using MagicVilla_Infrastructure.DbContexts;
using MagicVilla_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.Repositories
{
    public class VillaRepository : Repository<Villa, int>, IVillaRepository
    {
        public VillaRepository(IApplicationDbContext context) : base((DbContext)context)
        {

        }
    }
}