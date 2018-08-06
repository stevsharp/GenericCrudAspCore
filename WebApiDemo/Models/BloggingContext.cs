using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiDemo.Logging;

namespace WebApiDemo.Models
{

    public class BloggingContext : DbContext , IDbContext
    {
        protected readonly ILoggerFactory _loggerFactory;

        protected readonly ILogger _logger;

        protected readonly ILoggerProvider _loggerProvider;

        public BloggingContext(DbContextOptions<BloggingContext> options, ILoggerFactory loggerFactory ,
            ILogger logger , 
            ILoggerProvider loggerProvider)
            : base(options)
        {

            _loggerFactory = loggerFactory;
            _loggerProvider  = loggerProvider;
            _logger = logger;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public string ConnectionString
        {
            get{
                return string.Empty;
            }
        }
        /// <summary>
        /// Future Implementation 
        /// </summary>
        public DateTime UserProvider { get; private set; }
        private Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            _loggerFactory.AddProvider(_loggerProvider);
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                this.ApplyStateChanges();
                this.TrackChanges();
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                return -1;
            }
            catch (ValidationException ex)
            {
                return -1;
            }
            catch (DbUpdateException ex)
            {
                return -1;
            }

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyStateChanges();
            this.TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IAuditable)
                {
                    var auditable = entry.Entity as IAuditable;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedOn = TimestampProvider();
                        auditable.UpdatedOn = TimestampProvider();
                    }
                    else
                    {
                        auditable.UpdatedBy = UserProvider;
                        auditable.UpdatedOn = TimestampProvider();
                    }
                }
            }
        }
    }

}
