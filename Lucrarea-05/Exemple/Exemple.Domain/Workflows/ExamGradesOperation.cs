using Exemple.Domain.Models;
using static LanguageExt.Prelude;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using static Exemple.Domain.Models.ExamGrades;
using System.Threading.Tasks;
using static Exemple.Domain.Models.ShoppingCartChoice;

namespace Exemple.Domain
{
    public static class ShoppingCartOperation
    {
        public static IShoppingCart ValidateShoppingCart(Func<ProductCodeValidation, bool> checkProductExists, Func<ProductQuantityValidation, bool> checkIfEnoughStock, UnvalidatedShoppingCart shoppingCart)
        {
            List<ValidatedOrderLine> validatedCart = new();
            bool isValidList = true;
            string invalidReason = string.Empty;
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("WE ARE VALIDATING YOUR ORDER. PLEASE WAIT...");
            Console.WriteLine("------------------------------------------------------");

            foreach (var unvalidatedCart in shoppingCart.ProductsList)
            {
                ProductCodeValidation codeValidation = new(unvalidatedCart.productCode);
                ProductQuantityValidation quantityValidation = new(unvalidatedCart.productQuantity);

                if (unvalidatedCart.productCode == null || !checkProductExists(codeValidation))
                {
                    invalidReason = $"Invalid product code {unvalidatedCart.productCode}!";
                    isValidList = false;
                    break;
                }
                if (unvalidatedCart.productQuantity == 0 || !checkIfEnoughStock(quantityValidation))
                {
                    invalidReason = $"Invalid quantity {unvalidatedCart.productQuantity}!";
                    isValidList = false;
                    break;
                }
                if (isValidList)
                {
                    ProductCodeValidation thisProductCode = new(unvalidatedCart.productCode);
                    ProductQuantityValidation thisProductQuantity = new(unvalidatedCart.productQuantity);
                    ValidatedOrderLine validProduct = new(thisProductCode, thisProductQuantity );
                    validatedCart.Add(validProduct);
                }

            }

            if (isValidList)
            {
                return new ValidatedShoppingCart(validatedCart);
            }
            else
            {
                return new InvalidatedShoppingCart(shoppingCart.ProductsList, invalidReason);
            }

        }

        public static IShoppingCart CalculateTotalPrice(IShoppingCart cart) => cart.Match(
            whenEmptyShoppingCart: emptyCart => emptyCart,
            whenUnvalidatedShoppingCart: unvalidatedCart => unvalidatedCart,
            whenInvalidatedShoppingCart: invalidCart => invalidCart,
            whenCalculatedShoppingCart: calculatedCart => calculatedCart,
            whenPaidShoppingCart: paidCart => paidCart,
            whenValidatedShoppingCart: validCart =>
            {
                var calculateCart = validCart.ProductsList.Select(validCart =>
                                            new CalculatedPrice(validCart.productCode,
                                                                      validCart.productQuantity,
                                                                      validCart.productQuantity.CalculateTotalPrice()));

                return new CalculatedShoppingCart(calculateCart.ToList().AsReadOnly());
            }
        );

        public static IShoppingCart PayShoppingCart(IShoppingCart cart)
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("WE ARE PROCESSING YOUR PAYMENT. PLEASE WAIT... DONE!");
            Console.WriteLine("------------------------------------------------------");

            return cart.Match(
            whenEmptyShoppingCart: emptyCart => emptyCart,
            whenUnvalidatedShoppingCart: unvalidatedCart => unvalidatedCart,
            whenInvalidatedShoppingCart: invalidCart => invalidCart,
            whenValidatedShoppingCart: validatedCart => validatedCart,
            whenPaidShoppingCart: paidCart => paidCart,
            whenCalculatedShoppingCart: calculatedCart =>
            {
                StringBuilder csv = new();
                calculatedCart.ProductsList.Aggregate(csv, (export, cart) => export.AppendLine($"{cart.code.Value}, {cart.quantity.Value}, {cart.totalPrice}"));

                PaidShoppingCart paidShoppingCart = new(calculatedCart.ProductsList, csv.ToString(), DateTime.Now);

                return paidShoppingCart;
            });
        }
    }
}
