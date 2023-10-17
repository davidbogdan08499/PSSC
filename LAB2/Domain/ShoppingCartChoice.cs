using CSharp.Choices;
using static LAB2.Domain.ShoppingCartChoice;

namespace LAB2.Domain
{
	[AsChoice]
	public static partial class ShoppingCartChoice
	{
		public interface IShoppingCart { }
		public record UnvalidatedShoppingCart(ShoppingCartModel shoppingCart) : IShoppingCart;
		public record EmptyShoppingCart(ShoppingCartModel shoppingCartVar) : IShoppingCart;
		public record ValidatedShoppingCart(UnvalidatedShoppingCart unvalidatedShoppingCart) : IShoppingCart;
		public record PaidShoppingCart(ValidatedShoppingCart validatedShoppingCart, DateTime payDate) : IShoppingCart;
	}
}