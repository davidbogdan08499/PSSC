using Exemple.Domain.Models;
using Exemple.Domain.Repositories;
using LanguageExt;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Example.Data.Repositories
{
    public class OrderHeaderRepository: IOrderHeaderRepository

    {
        private readonly ProductsContext productsContext;

        public OrderHeaderRepository(ProductsContext productsContext)
        {
            this.productsContext = productsContext;  
        }

        //public TryAsync<OrderHeaderId> TryGetExistingOrderHeader(int orderHeaderIdToCheck) => async () =>
        //{
        //    var orderHeader = await productsContext.OrderHeaders
        //                                      .Where(orderH => orderHeaderIdToCheck.Contains(orderH.OrderHeaderId))
        //                                      .AsNoTracking()
        //                                      .ToListAsync();
        //    return orderHeader.Select(orderH => new OrderHeaderId(orderH.OrderHeaderId));
        //};
    }
}
