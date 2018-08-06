using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApiDemo.Models
{
    public interface IDbContext
    {
        string ConnectionString { get; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        EntityEntry Entry(object o);
        void Dispose();
    }

}
