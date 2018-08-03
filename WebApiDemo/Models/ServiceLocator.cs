using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApiDemo.Models
{
    public class ServiceLocator
    {
        private IServiceProvider _currentServiceProvider;
        private static ServiceProvider _serviceProvider;

        public ServiceLocator(IServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        public static ServiceLocator Current
        {
            get
            {
                return new ServiceLocator(_serviceProvider);
            }
        }

        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _currentServiceProvider.GetService<TService>();
        }
    }
}
