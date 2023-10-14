using CSharp.Choices;

namespace LAB1.Domain
{
    [AsChoice]
    public static  class Quantity
    {
        public interface IQuantity
        {
        }

        public record Unit(int Number) : IQuantity;

        public record Kilograms(double NumberOfKg) : IQuantity;

        public record Undefined(string UndefinedString): IQuantity;
    }
}