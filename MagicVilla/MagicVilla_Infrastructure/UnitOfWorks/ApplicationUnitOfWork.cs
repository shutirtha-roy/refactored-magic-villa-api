using MagicVilla_Infrastructure.DbContexts;
using MagicVilla_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IVillaRepository Villas { get; private set; }
        public IVillaNumberRepository VillaNumbers { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext, 
            IVillaRepository villaRepository, IVillaNumberRepository villaNumberRepository) : base((DbContext)dbContext)
        {
            Villas = villaRepository;
            VillaNumbers = villaNumberRepository;
        }
    }
}