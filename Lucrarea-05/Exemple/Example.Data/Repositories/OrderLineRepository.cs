using System;
namespace Example.Data.Repositories
{
    public class OrderLineRepository
    {

        private readonly ProductsContext productsContext;

        public OrderLineRepository(ProductsContext productsContext)
        {
            this.productsContext = productsContext;
        }
    }
}

