using static LAB1.Domain.Quantity;
using static LAB1.Domain.Order;
using System;
using CSharp.Choices;
using LAB1.Domain;

internal class Program
{
    private static IQuantity ConvertToQuantity(string sal)
    {
        if (Int32.TryParse(sal, out int units)) return new Unit(units);
        else if (Double.TryParse(sal, out double kgs)) return new Kilograms(kgs);
        else return new Undefined(sal);

    }

    private static Unit print(Unit units)
    {
        Console.WriteLine(units.Number + "u");
        return units;
    }
    private static Product ProductUserInterface()
    {
        Console.WriteLine("Enter the product name: ");

        Console.Write("Enter the product code: ");
        string productID = Console.ReadLine();

        Console.WriteLine("----");

        Console.Write("Quantity of the desired product: ");
        string productQuantity = Console.ReadLine();

        IQuantity quantity = ConvertToQuantity(productQuantity);

        quantity.Match(
            whenKilograms: kilograms => kilograms,
            whenUndefined: undefined => undefined,
            whenUnits: units => print(units)
            );

        Product product = new Product
        {
            ID = productID,
            Quantity = quantity,
        };

        return product;
    }

    private static void Main(string[] args)
    {

        Contact person = new Contact
        {
           FirstName="Bogdan",
           LastName="David",
           Email="davidbogdan970@gmail.com",
           Phone="0756887280",
           Address="Aleea Studentilor,21C"
        };

        Console.WriteLine(person.ToString());

        Console.WriteLine("-----------------------------------------");


        Product returnFromPUI = ProductUserInterface();

        Order newOrder = new Order
        {
            Person = person,
            Products = new List<Product>()
        };

        newOrder.Products.Add(returnFromPUI);

        string response;
        do
        {
            Console.WriteLine("Do you wanna add another product? (y/n): ");
            response = Console.ReadLine();
            if (response == "n") break;

            Product anotherReturnFromPUI = ProductUserInterface();
            newOrder.Products.Add(anotherReturnFromPUI);


        } while (response == "y");

        Console.WriteLine(newOrder.ToString());
    }

}

