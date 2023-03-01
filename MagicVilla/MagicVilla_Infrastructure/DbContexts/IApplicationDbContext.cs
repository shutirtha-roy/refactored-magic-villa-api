using MagicVilla_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}