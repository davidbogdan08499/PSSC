using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3.Domain
{
    [AsChoice]
    public static partial class ShoppingCartChoice
    {
        public interface IShoppingCart { }

        public record UnvalidatedShoppingCart : IShoppingCart
        {
            public UnvalidatedShoppingCart(IReadOnlyCollection<UnvalidatedProduct> productsList)
            {
                ProductsList = productsList;
            }

            public IReadOnlyCollection<UnvalidatedProduct> ProductsList { get; }
        }

        public record EmptyShoppingCart : IShoppingCart
        {
            public EmptyShoppingCart(ShoppingCart shoppingCartVar)
            {
                ShoppingCartVar = shoppingCartVar;
            }

            public ShoppingCart ShoppingCartVar { get; }
        }

        public record InvalidatedShoppingCart : IShoppingCart
        {
            public InvalidatedShoppingCart(IReadOnlyCollection<UnvalidatedProduct> productsList, string reason)
            {
                ProductsList = productsList;
                Reason = reason;
            }

            public IReadOnlyCollection<UnvalidatedProduct> ProductsList { get; }
            public string Reason { get; }
        }

        public record ValidatedShoppingCart : IShoppingCart
        {
            public ValidatedShoppingCart(IReadOnlyCollection<ValidatedProduct> productsList)
            {
                ProductsList = productsList;
            }

            public IReadOnlyCollection<ValidatedProduct> ProductsList { get; }
        }

        public record CalculatedShoppingCart : IShoppingCart
        {
            public CalculatedShoppingCart(IReadOnlyCollection<CalculatedPrice> productsList)
            {
                ProductsList = productsList;
            }

            public IReadOnlyCollection<CalculatedPrice> ProductsList { get; }
        }

        public record PaidShoppingCart : IShoppingCart
        {
            public PaidShoppingCart(IReadOnlyCollection<CalculatedPrice> productsList, string csv, DateTime payDate)
            {
                ProductsList = productsList;
                Csv = csv;
                PayDate = payDate;
            }

            public IReadOnlyCollection<CalculatedPrice> ProductsList { get; }
            public string Csv { get; }
            public DateTime PayDate { get; }
        }
    }
}
