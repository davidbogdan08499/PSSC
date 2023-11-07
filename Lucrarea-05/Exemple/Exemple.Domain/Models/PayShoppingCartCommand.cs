using System.Collections.Generic;

namespace Exemple.Domain.Models
{
    public record PayShoppingCartCommand
    {

        public PayShoppingCartCommand(IReadOnlyCollection<UnvalidatedOrderLine> inputShoppingCart)
        {
            InputShoppingCart = inputShoppingCart;
        }

        public IReadOnlyCollection<UnvalidatedOrderLine> InputShoppingCart { get; }
    }
}
