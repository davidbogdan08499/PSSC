using System;
using CSharp.Choices;

namespace LAB3.Domain
{
	[AsChoice]
	public static partial class ShoppingCartPaidEvent
	{
		public interface IShoppingCartPaidEvent { }

		public record ShoppingCartPaidSucceededEvent : IShoppingCartPaidEvent
		{
			public string Csv { get; }
			public DateTime PaidDate { get; }

			internal ShoppingCartPaidSucceededEvent(string csv, DateTime paidDate)
			{
				Csv = csv;
				PaidDate = paidDate;
			}
		}

        public record ShoppingCartPaidFailedEvent : IShoppingCartPaidEvent
        {
            public string Reason { get; }

            internal ShoppingCartPaidFailedEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}

