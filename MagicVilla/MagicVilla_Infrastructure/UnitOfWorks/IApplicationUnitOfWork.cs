using MagicVilla_Infrastructure.Repositories;

namespace MagicVilla_Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IVillaRepository Villas { get; }
        IVillaNumberRepository VillaNumbers { get; }
        IUserRepository Users { get; }
    }
}