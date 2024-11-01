using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter numbers separated by comma to add: ");
        string input = Console.ReadLine();
        Console.WriteLine("Sum: " + Add(input));
    }

    public static int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numberArray = numbers.Split(',').Select(n =>
        {
            int number;
            return int.TryParse(n, out number) ? number : 0;
        }).ToArray();

        if (numberArray.Length > 2)
        {
            throw new ArgumentException("The input string should contain a maximum of 2 numbers");
        }

        return numberArray.Sum();
    }
}