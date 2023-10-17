using LAB3.Domain;
using System;
using System.Collections.Generic;
using static LAB3.Domain.ShoppingCartChoice;

namespace LAB3
{
    class Program
    {

        static void Main(string[] args)
        {
            Client newClient = new()
            {
                name = "Bogdan",
                adress = "Aleea Studentilor,21C"
            };

            ShoppingCart newShoppingCart = new()
            {
                client = newClient,
                products = new List<Product>()
            };

            var listOfProducts = ReadListOfProducts().ToArray();
            PayShoppingCartCommand command = new(listOfProducts);
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

        private static List<UnvalidatedProduct> ReadListOfProducts()
        {
            List<UnvalidatedProduct> listOfProducts = new();

            string response;
            do
            {
                //read product code and quantity
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
                listOfProducts.Add(new(productCode, productQuantityInt));

                response = ReadValue("Do you want to add another product to your shopping cart? (y/n): ");

            } while (response.ToLower() == "y");

            return listOfProducts;
        }




        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
