using System;
using System.Diagnostics;

namespace LAB3.Domain
{
    public record ValidatedProduct(ProductCodeValidation productCode, ProductQuantityValidation productQuantity, double price=4.99);
}

