using System;
namespace Exemple.Domain.Models
{
	public record OrderHeaderId(int id)
	{
		public int OrderId { get; set; }

        private static bool IsValid(int id) => id > 0;
    }
}

