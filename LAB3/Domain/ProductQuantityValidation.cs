using System;
using System.Text.RegularExpressions;

namespace LAB3.Domain
{
    public class ProductQuantityValidation
    {
        public static int stock = 25;

        public int Value { get; set; }

        public ProductQuantityValidation(int value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductQuantityException("");
            }
        }

        public double CalculateTotalPrice()
        {
            double totalPrice =  Value * 4.99;
            return totalPrice;
        }

        private static bool IsValid(int stringValue) => stringValue < stock;


        public static bool TryParse(int intValue, out ProductQuantityValidation productQuantity)
        {
            bool isValid = false;
            productQuantity = null;

            if (IsValid(intValue))
            {
                isValid = true;
                productQuantity = new(intValue);
            }

            return isValid;
        }
    }
}

