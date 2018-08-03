using System;
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


    public class BloggingContext : DbContext , IDbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public string ConnectionString
        {
            get{
                return string.Empty;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyStateChanges();
            return base.SaveChanges();
        }
    }

}
