using MagicVilla_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<Villa> Villas { get; set; }
    }
}