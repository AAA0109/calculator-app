using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter numbers separated by comma to add: ");
        string input = Console.ReadLine();
        input = input.Replace("\\n", "\n");
        Console.WriteLine("Sum: " + Add(input));
    }

    public static int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numberArray = numbers.Split(new[] { '\n', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        // Convert the valid numbers to integers and calculate the sum
        return numberArray.Sum();
    }
}