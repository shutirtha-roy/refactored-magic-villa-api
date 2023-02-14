using MagicVilla_Infrastructure.DbContexts;
using MagicVilla_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.Repositories
{
    public class VillaNumberRepository : Repository<VillaNumber, int>, IVillaNumberRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public VillaNumberRepository(IApplicationDbContext context) : base((DbContext)context)
        {
            _dbContext = context;
        }

        public async Task<VillaNumber> GetVillaNumberById(int villaNo)
        {
            return _dbContext.VillaNumbers.AsNoTracking().Include(x => x.Villa).Where(y => y.VillaNo == villaNo).FirstOrDefault();
        }

        public async Task<List<VillaNumber>> GetAllVillaNumbers()
        {
            return _dbContext.VillaNumbers.Include(x => x.Villa).ToList();
        }
    }
}