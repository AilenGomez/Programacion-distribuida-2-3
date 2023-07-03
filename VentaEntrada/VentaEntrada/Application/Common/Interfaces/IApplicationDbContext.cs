using Microsoft.EntityFrameworkCore;
using PuertaDeEntrada.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace VentaEntrada.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task ReloadEntityAsync<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public void Dispose();
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}