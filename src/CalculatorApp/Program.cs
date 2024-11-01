using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter numbers separated by comma to add: ");
        string input = Console.ReadLine();
        input = input.Replace("\\n", "\n");
        try
        {
            Console.WriteLine("Sum: " + Add(input));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numberArray = numbers.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Parse numbers and check for negatives
        var parsedNumbers = numberArray.Select(int.Parse).ToList();
        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();

        // If any negative numbers are found, throw an exception
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers are not allowed: " + string.Join(", ", negativeNumbers));
        }

        return parsedNumbers.Sum();
    }
}