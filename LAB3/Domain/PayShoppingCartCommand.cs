using System;
using LAB3.Domain;

namespace LAB3.Domain
{
	public record PayShoppingCartCommand
	{
		
		public PayShoppingCartCommand(IReadOnlyCollection<UnvalidatedProduct> inputShoppingCart)
		{
			InputShoppingCart = inputShoppingCart;
		}

		public IReadOnlyCollection<UnvalidatedProduct> InputShoppingCart { get;  }
	}
}

