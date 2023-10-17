using System;
using System.Diagnostics;

namespace LAB3.Domain
{
	public record CalculatedPrice(ProductCodeValidation code, ProductQuantityValidation quantity, double totalPrice, double price= 4.99);
}

