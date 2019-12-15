using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace PracticeLinqEfCore
{
    class Program
    {


        private static string ConnectionString;
    

        static void Main(string[] args)
        {

            var _configureNamedOptions = new ConfigureNamedOptions<ConsoleLoggerOptions>("", null);
            var _optionsFactory = new OptionsFactory<ConsoleLoggerOptions>(new[] { _configureNamedOptions }, Enumerable.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>());
            var _optionsMonitor = new OptionsMonitor<ConsoleLoggerOptions>(_optionsFactory, Enumerable.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>(), new OptionsCache<ConsoleLoggerOptions>());
            var _loggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider(_optionsMonitor) }, new LoggerFilterOptions { MinLevel = LogLevel.Information });

            var optionsBuilder = new DbContextOptionsBuilder<LinqPracticeContext>();
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(ConnectionString);
            using (var context = new LinqPracticeContext(optionsBuilder.Options))
            {
               
            }

            Console.ReadLine();
        }





    }
}
