using static LAB1.Domain.Quantity;

namespace LAB1.Domain {

    public record Product
    {
        public string ID { get; set; }
        public IQuantity Quantity { get; set; }

        public override string ToString()
        {
            return $"Product id:{ID} and quantity:{Quantity}";
        }

    }

}