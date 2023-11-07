namespace Exemple.Domain.Models
{
    public record CalculatedPrice(ProductCodeValidation code, ProductQuantityValidation quantity, double totalPrice, double price=4.99);

}
