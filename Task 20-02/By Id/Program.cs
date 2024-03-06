using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    class LibraryItem
    {
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Borrower { get; set; }
    }
        
    static void Main(string[] args)
    {
        List<LibraryItem> libraryItems = new List<LibraryItem>
        {
            new LibraryItem { ItemNumber = 1, Name = "Item 1", Creator = "Creator 1", Borrower = "Borrower 1" },
            new LibraryItem { ItemNumber = 2, Name = "Item 2", Creator = "Creator 2", Borrower = "Borrower 2" },
            new LibraryItem { ItemNumber = 3, Name = "Item 3", Creator = "Creator 3", Borrower = "Borrower 3" }
        };

        Console.Write("Enter borrower's name: ");
        string borrowerName = Console.ReadLine();

        var borrowedItem = (from item in libraryItems
                            where item.Borrower.Equals(borrowerName, StringComparison.OrdinalIgnoreCase)
                            select item).FirstOrDefault();

        if (borrowedItem != null)
        {
            Console.WriteLine($"Item details for borrower {borrowerName}:");
            Console.WriteLine($"Item Number: {borrowedItem.ItemNumber}");
            Console.WriteLine($"Name: {borrowedItem.Name}");
            Console.WriteLine($"Creator: {borrowedItem.Creator}");
        }
        else
        {
            Console.WriteLine($"No item borrowed by {borrowerName}.");
        }
    }
}
