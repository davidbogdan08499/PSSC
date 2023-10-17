using System;
using static LAB3.Domain.ShoppingCartChoice;
using static LAB3.Domain.ShoppingCartOperation;
using static LAB3.Domain.ShoppingCartPaidEvent;

namespace LAB3.Domain
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

