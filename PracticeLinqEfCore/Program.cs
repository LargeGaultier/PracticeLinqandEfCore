using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;

namespace PracticeLinqEfCore
{
    class Program
    {


        private static string ConnectionString ;


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
                //Exercice1(context);
                //Console.ReadLine();
                //Exercice2(context);
                //Console.ReadLine();
                //Exercice2WithInclude(context);
                //Console.ReadLine();
                //Exercice3(context);
                //Console.ReadLine();
                //Exercice4(context);
                // Console.ReadLine();
                //Exercice5(context);
                //Console.ReadLine();
                //Exercice6(context);
                //Console.ReadLine();
                //Exercice7(context,2,10);
                //Console.ReadLine();
                //Exercice8(context);
                //Console.ReadLine();
                //Exercice9(context);
                Console.ReadLine();
                Exercice10(context);
                Exercice10Bis(context);
                Exercice10ter(context);
            }

            Console.ReadLine();
        }

        private static void Exercice1(LinqPracticeContext context)
        {
            var query = context.Biere.Select(b => new { BrandName = b.NomMarque, Version = b.Version });

            foreach (var item in query)
            {
                Console.WriteLine($"beer name : {item.BrandName} , version : {item.Version}");
            }
        }

        private static void Exercice2(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere.Select(x => new { BrandName = x.NomMarque, BreweryName = x.NomMarqueNavigation.CodeBrasserieNavigation.NomBrasserie }).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($"nom de la biere : {item.BrandName} - brewery name : {item.BreweryName}");
            }
        }

        private static void Exercice2WithInclude(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere.Include(biere => biere.NomMarqueNavigation).ThenInclude(x => x.CodeBrasserieNavigation).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($"beer name : {item.NomMarque}- brewery name : {item.NomMarqueNavigation?.CodeBrasserieNavigation?.NomBrasserie}");
            }


        }

        private static void Exercice3(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere
                .Where(b => b.NomMarqueNavigation.CodeBrasserieNavigation.NomRegionNavigation.NomPaysNavigation.NomPays == "France")
                .Select(x => new { BrandName = x.NomMarque, BreweryName = x.NomMarqueNavigation.CodeBrasserieNavigation.NomBrasserie }).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($"beer name : {item.BrandName} - brewery name : {item.BreweryName}");
            }
        }

        private static void Exercice4(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = from bieres in context.Biere
                        group new { bieres.CouleurBiere } by (bieres.CouleurBiere) into Colors
                        select new { Colors = Colors.Key, ColorCount = Colors.Count() };
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" beer color : {item.Colors} - colorNumber : {item.ColorCount}");
            }
        }

       

        private static void Exercice5(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere.OrderByDescending(b => b.TauxAlcool).Take(10).Select(b => new { BeerName = b.NomMarque, Version = b.Version }).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" beer : {item.BeerName} - version : {item.Version}");
            }
        }

        private static void Exercice6(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Region.Where(reg => !context.Brasserie.Any(br => reg.NomRegion == br.NomRegion)).Select(reg => reg.NomRegion).Distinct(); ;
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" pays : {item} producteur de bière");
            }
        }


        private static void Exercice7(LinqPracticeContext context, int pageNumber, int numberPerPage)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere.Skip(pageNumber - 1 * numberPerPage).Take(numberPerPage).Select(b => new { BeerName = b.NomMarque, Version = b.Version }).ToList(); 
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" beer : {item.BeerName} - version : {item.Version}");
            }
        }



        private static void Exercice9(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Biere.Where(b => b.NomMarqueNavigation.CodeBrasserieNavigation.NomRegionNavigation.NomPays == "France")
                .GroupBy(b =>  b.NomMarqueNavigation.CodeBrasserieNavigation.NomRegionNavigation.NomPays)
                .Select(g => new{ countryName = g.Key, alcoholAverage =g.Average(b => b.TauxAlcool)}).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" Country Name : {item.countryName} - Alcohol average {item.alcoholAverage}");
            }
        }

        private static void Exercice8(LinqPracticeContext context)
        {
            //Stopwatch _localStopWatch = Stopwatch.StartNew();
            //var query = context.Type
            //    .GroupBy(t => new { countryName = t.Biere.SelectMany(x => x.NomMarqueNavigation.CodeBrasserieNavigation.NomRegionNavigation.NomPays), idType = t.NomType })
            //    .Select(g => new { countryName = g.Key.countryName, type = g.Key.idType, nbType = g.Count(x=> x.NomType == g.Key.idType) }).ToList();
            //_localStopWatch.Stop();
            //Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            //foreach (var item in query)
            //{
            //    Console.WriteLine($" Country Name : {item.countryName}- type {item.type} - nb {item.nbType}");
            //}
        }

        private static void Exercice10(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context.Brasserie.Where(b =>
                 context.Biere.Select(bi => bi.CouleurBiere).Distinct()
                 .All(couleur =>
                     b.Marque.Any(marque => marque.Biere
                         .Any(beer => beer.CouleurBiere == couleur))))
                 .Select(br => br.NomBrasserie).OrderBy(o => o).ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" bewery Name : {item}");
            }
        }

        private static void Exercice10Bis(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
          
            var query = context.Biere
               .GroupBy(b => new
               {
                   b.NomMarqueNavigation.CodeBrasserieNavigation.CodeBrasserie,
                   b.CouleurBiere
               })
                .Select(g => new { g.Key, Count = g.Count() })
                .GroupBy(g => g.Key.CodeBrasserie)
                .Select(g => new { g.Key, Count = g.Count() })
                .Where(g => g.Count == context
                    .Biere.Select(bi => new { bi.CouleurBiere })
                    .Distinct()
                    .Count())
                .ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" bewery Name : {item.Key}");
            }

        //Solution SQL :
        //    SELECT[b0].[CodeBrasserie]
        //  FROM[Biere] AS[b]
        //  INNER JOIN[Marque] AS[m] ON[b].[NomMarque] = [m].[NomMarque]
        //  INNER JOIN[Brasserie] AS[b0] ON[m].[CodeBrasserie] = [b0].[CodeBrasserie]
        //  GROUP BY[b0].[CodeBrasserie]
        //HAVING COUNT(distinct b.CouleurBiere) = 3
 
        }

        private static void Exercice10ter(LinqPracticeContext context)
        {
            Stopwatch _localStopWatch = Stopwatch.StartNew();
            var query = context
                .Brasserie
                .Where(
                    b =>
                     context
                     .Biere
                     .Select(bi => bi.CouleurBiere)
                     .Distinct()
                     .All(couleur =>
                            b.Marque.Any(
                                m => m.Biere.Any(bier => bier.CouleurBiere == couleur)
                            )
                          )
                     )
                     .Select(b => b.NomBrasserie)
                     .ToList();
            _localStopWatch.Stop();
            Console.WriteLine($" processing Time : {_localStopWatch.ElapsedMilliseconds} ms");
            foreach (var item in query)
            {
                Console.WriteLine($" bewery Name : {item}");
            }
        }

    }
}
