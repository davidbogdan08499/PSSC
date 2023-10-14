
using Microsoft.CodeAnalysis;
using static LAB1.Domain.Contact;
using static LAB1.Domain.Product;

namespace LAB1.Domain
{
    public record Order
    {
        public Contact Person { get; set; }

        public List<Product> Products { get; set;}

        public string ToStringProducts()
        {
            string output=string.Join("\n", Products);
            return output;
        }

        public override string ToString()
        {
            return $"Person:{Person} has the following orders:{ToStringProducts()}";
        }
    }
}