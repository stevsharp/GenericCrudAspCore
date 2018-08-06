using System;
using System.Collections;

namespace WebApiDemo.Models
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;

        private bool disposed;

        private Hashtable repositories;

        public string ConnectionString
        {
            get
            {
                return context.ConnectionString;
            }
        }

        public UnitOfWork(IDbContext contextPar)
        {
            context = contextPar;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            int save = context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed) if (disposing) context.Dispose();

            disposed = true;
        }


        public IRepository<T> Repository<T>() where T : class
        {
            return ServiceLocator.Current.GetInstance<IRepository<T>>();
        }
    }
}
