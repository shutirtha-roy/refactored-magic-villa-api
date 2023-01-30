using MagicVilla_Infrastructure.UnitOfWorks;

namespace MagicVilla_Infrastructure.Services
{
    public class VillaService : IVillaService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public VillaService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
    }
}