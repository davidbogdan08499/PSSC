namespace LAB2.Domain
{
    public class ShoppingCartModel
    {
        public ClientModel client { get; set; }
        public List<ProductModel> products { get; set; }

        public ShoppingCartModel() { }
    }
}