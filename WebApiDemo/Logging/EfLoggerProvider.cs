using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Logging
{
    public class EfLoggerProvider : ILoggerProvider
    {

        private readonly ILogger _ILogger;
        public EfLoggerProvider(ILogger Logger)
        {
            _ILogger = Logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _ILogger;
        }

        public void Dispose()
        {
           
        }
    }

    public class EfLogger : ILogger
    {
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //File.AppendAllText(@"C:\temp\log.txt", formatter(state, exception));
            Console.WriteLine(formatter(state, exception));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
