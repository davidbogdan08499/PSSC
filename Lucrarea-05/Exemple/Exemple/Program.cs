using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using Exemple.Domain;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;
using Example.Data.Repositories;
using Example.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exemple
{
    class Program
    {
        private static string ConnectionString = "Server=localhost;Database=OrderDataBase2;Trusted_Connection=True;";

        static async Task Main(string[] args)
        {
            using ILoggerFactory loggerFactory = ConfigureLoggerFactory();
            ILogger<PayShoppingCartWorkflow> logger = loggerFactory.CreateLogger<PayShoppingCartWorkflow>();

            var listOfProducts = ReadListOfGrades().ToArray();
            PayShoppingCartCommand command = new PayShoppingCartCommand(listOfProducts);
            var dbContextBuilder = new DbContextOptionsBuilder<ProductsContext>()
                                                .UseSqlServer(ConnectionString)
                                                .UseLoggerFactory(loggerFactory);

            ProductsContext productContext = new ProductsContext(dbContextBuilder.Options);
            OrderHeaderRepository orderHeaderRepository = new OrderHeaderRepository(productContext);
            //ProductsRepository productsRepository = new ProductsRepository(productContext);

            PayShoppingCartWorkflow workflow = new PayShoppingCartWorkflow();
            var result = workflow.Execute(command, (productCode) => true, (quantity) => true);

            result.Match(
                    whenShoppingCartPaidFailedEvent: @event =>
                    {
                        Console.WriteLine($"Failed payment: {@event.Reason}");
                        return @event;
                    },
                    whenShoppingCartPaidSucceededEvent: @event =>
                    {
                        Console.WriteLine($"Successful payment: ");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );
        }

        private static ILoggerFactory ConfigureLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
                                builder.AddSimpleConsole(options =>
                                {
                                    options.IncludeScopes = true;
                                    options.SingleLine = true;
                                    options.TimestampFormat = "hh:mm:ss ";
                                })
                                .AddProvider(new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()));
        }

        private static List<UnvalidatedOrderLine> ReadListOfGrades()
        {
            List<UnvalidatedOrderLine> listOfProducts = new();

            do
            {
                var productCode = ReadValue("Product Code: ");
                if (string.IsNullOrEmpty(productCode))
                {
                    break;
                }

                var productQuantity = ReadValue("Product Quantity: ");
                if (string.IsNullOrEmpty(productQuantity))
                {
                    break;
                }
                int productQuantityInt = Int32.Parse(productQuantity);

                var productPrice = ReadValue("Product Price: ");
                if (string.IsNullOrEmpty(productPrice))
                {
                    break;
                }
                double productPriceDouble = Double.Parse(productPrice);

                listOfProducts.Add(new(productCode, productQuantityInt, productPriceDouble));


            } while (true);
            return listOfProducts;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

    }
}
