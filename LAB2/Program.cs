using  LAB2.Domain;
using static LAB2.Domain.ShoppingCartChoice;


namespace LAB2
{
    class MainProgram
    {
        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
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

                listOfProducts.Add(new(productCode, productQuantity));

                response = ReadValue("Do you want to add another product to your shopping cart? (y/n): ");

            } while (response == "y");

            return listOfProducts;
        }

        private static IShoppingCart ValidateShoppingCart(UnvalidatedShoppingCart unvalidatedShopping)
        {
            Console.WriteLine("VALIDATING YOUR ORDER");
            return new ValidatedShoppingCart(unvalidatedShopping);

        }

        private static IShoppingCart PayShoppingCart(ValidatedShoppingCart validShoppingCart)
        {
            Console.WriteLine(" PROCESSING....");
            return new PaidShoppingCart(validShoppingCart, DateTime.Now);
        }



        static void Main(string[] args)
        {
            ClientModel client = new ClientModel()
            {
                name= "Bogdan",
                address="Emil Isac,6"
            };

            ShoppingCartModel shoppingCart = new ShoppingCartModel()
            {
                client = client,
                products=new List<ProductModel>()
            };

            var listOfProducts = ReadListOfProducts().ToArray();
            UnvalidatedShoppingCart unvalidatedShoppingCart = new UnvalidatedShoppingCart(shoppingCart);
            IShoppingCart result = ValidateShoppingCart(unvalidatedShoppingCart);
            result.Match(
                whenEmptyShoppingCart: emptyResult => emptyResult,
                whenUnvalidatedShoppingCart: unvalidatedResult => unvalidatedShoppingCart,
                whenPaidShoppingCart: paidResult => paidResult,
                whenValidatedShoppingCart: validatedResult => PayShoppingCart(validatedResult)
                );

        }
    }
}
