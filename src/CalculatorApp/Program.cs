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

        string numberSection = numbers;
        List<string> delimiters = new List<string> { ",", "\n" };

        // Check for custom delimiter syntax
        if (numbers.StartsWith("//[") && numbers.Contains("]\n"))
        {
            // Find the end of the delimiter declaration
            int endDelimiterIndex = numbers.IndexOf("]\n");
            string customDelimiter = numbers.Substring(3, endDelimiterIndex - 3); // Extract the custom delimiter
            delimiters.Add(customDelimiter); // Add the custom delimiter to the list
            numberSection = numbers.Substring(endDelimiterIndex + 2); // Remove the custom delimiter part from the input
        }
        else if (numbers.StartsWith("//") && numbers.Length > 4 && numbers[3] == '\n')
        {
            char singleCharDelimiter = numbers[2];
            delimiters.Add(singleCharDelimiter.ToString()); // Add single character custom delimiter
            numberSection = numbers.Substring(4);
        }

        // Split by custom delimiter, comma, or newline characters
        var numberArray = numberSection.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

        // Parse numbers and apply constraints
        var parsedNumbers = new List<int>();

        foreach (var number in numberArray)
        {
            // Try parsing each item and add valid integers to the list
            if (int.TryParse(number, out int parsedNumber))
            {
                parsedNumbers.Add(parsedNumber);
            }
        }

        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();

        // If any negative numbers are found, throw an exception
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers are not allowed: " + string.Join(", ", negativeNumbers));
        }

        // Exclude numbers greater than 1000 and calculate the sum
        return parsedNumbers.Where(n  => n <= 1000).Sum();
    }
}