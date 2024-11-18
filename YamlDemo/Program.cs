// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System.Net.Sockets;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

var address = new Address
{
    street = "123 Main St",
    city = "Anytown",
    state = "CA",
};

var person = new Person
{
    firstName = "John",
    lastName = "Doe",
    address = address,
};

var receipt = new Receipt
{
    address = address,
    date = new DateTime(2007, 1, 1),
    items = new List<Item>{
        new Item { name = "item1", price = 10.00 },
        new Item { name = "item2", price = 20.00 },
    },
    person = person,
};

var serializer = new Serializer();
var yaml = serializer.Serialize(receipt);
File.WriteAllText("receipt.yaml", yaml);

internal class Receipt
{
    public Address address { get; set; }
    public DateTime date { get; set; }
    public List<Item> items { get; set; }
    public Person person { get; set; }
}

public class Item
{   
    public string name { get; set; }
    public double price { get; set; }
}

public class Address
{
    public string street { get; set; }
    public string city { get; set; }
    public string state { get; set; }
}

public class Person
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public Address address { get; set; }
}

