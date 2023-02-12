using MagicVilla_Infrastructure.DbContexts;
using MagicVilla_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.Repositories
{
    public class VillaNumberRepository : Repository<VillaNumber, int>, IVillaNumberRepository
    {
        public VillaNumberRepository(IApplicationDbContext context) : base((DbContext)context)
        {

        }
    }
}