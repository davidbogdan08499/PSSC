using Exemple.Domain.Models;
//using static Exemple.Domain.Models.ExamGradesPublishedEvent;
using System;
using LanguageExt;
using System.Threading.Tasks;
using System.Collections.Generic;
using Exemple.Domain.Repositories;
using System.Linq;
using static LanguageExt.Prelude;
using Microsoft.Extensions.Logging;
using static Exemple.Domain.Models.ShoppingCartChoice;
using static Exemple.Domain.Models.ShoppingCartPaidEvent;
using static Exemple.Domain.ShoppingCartOperation;

namespace Exemple.Domain
{
    public class PayShoppingCartWorkflow
    {
        public IShoppingCartPaidEvent Execute(PayShoppingCartCommand command, Func<ProductCodeValidation, bool> checkProductExists, Func<ProductQuantityValidation, bool> checkIfEnoughStock)
        {
            UnvalidatedShoppingCart unvalidatedCart = new UnvalidatedShoppingCart(command.InputShoppingCart);
            IShoppingCart cart = ValidateShoppingCart(checkProductExists, checkIfEnoughStock, unvalidatedCart);
            cart = CalculateTotalPrice(cart);
            cart = PayShoppingCart(cart);

            return cart.Match(
                    whenEmptyShoppingCart: emptyCart => new ShoppingCartPaidFailedEvent("Unexpected empty state") as IShoppingCartPaidEvent,
                    whenUnvalidatedShoppingCart: unvalidatedCart => new ShoppingCartPaidFailedEvent("Unexpected unvalidated state"),
                    whenInvalidatedShoppingCart: invalidCart => new ShoppingCartPaidFailedEvent(invalidCart.Reason),
                    whenValidatedShoppingCart: validatedCart => new ShoppingCartPaidFailedEvent("Unexpected validated state"),
                    whenCalculatedShoppingCart: calculatedCart => new ShoppingCartPaidFailedEvent("Unexpected calculated state"),
                    whenPaidShoppingCart: paidCart => new ShoppingCartPaidSucceededEvent(paidCart.Csv, paidCart.PayDate)
                );
        }
    }
}
