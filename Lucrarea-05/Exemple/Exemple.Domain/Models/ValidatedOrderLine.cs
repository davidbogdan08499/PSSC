namespace Exemple.Domain.Models
{
    public record ValidatedOrderLine(ProductCodeValidation productCode, ProductQuantityValidation productQuantity, double productPrice=4.99);
}
