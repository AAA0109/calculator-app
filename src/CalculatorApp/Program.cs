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
            Console.WriteLine(Add(input));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static string Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return "0 = 0";
        }

        string numberSection = numbers;
        List<string> delimiters = new List<string> { ",", "\n" };

        // Check for custom delimiter syntax
        if (numbers.StartsWith("//"))
        {
            int endDelimiterIndex = numbers.IndexOf("\n");
            string delimiterPart = numbers.Substring(2, endDelimiterIndex - 2);

            // Handle multiple delimiters
            if (delimiterPart.Contains("]["))
            {
                delimiters.AddRange(delimiterPart.Split(new[] { "][" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(d => d.Trim('[', ']')));
            }
            else
            {
                // Single character or single length delimiter
                delimiters.Add(delimiterPart.Trim('[', ']'));
            }

            numberSection = numbers.Substring(endDelimiterIndex + 1); // Remove the delimiter part from the input
        }

        // Split by custom delimiter, comma, or newline characters
        var numberArray = numberSection.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

        // Parse numbers and apply constraints
        var parsedNumbers = new List<int>();
        var formulaParts = new List<string>();

        foreach (var number in numberArray)
        {
            // Try parsing each item and add valid integers to the list
            if (int.TryParse(number, out int parsedNumber))
            {
                if (parsedNumber <= 1000)
                {
                    parsedNumbers.Add(parsedNumber);
                    formulaParts.Add(parsedNumber.ToString());
                }
                else
                {
                    formulaParts.Add("0");
                }
            }
            else
            {
                formulaParts.Add("0");
            }
        }

        var negativeNumbers = parsedNumbers.Where(n => n < 0).ToList();

        // If any negative numbers are found, throw an exception
        if (negativeNumbers.Any())
        {
            throw new ArgumentException("Negative numbers are not allowed: " + string.Join(", ", negativeNumbers));
        }

        int totalSum = parsedNumbers.Sum();

        // Create the formula string
        string formula = string.Join("+", formulaParts);
        return $"{formula} = {totalSum}";
    }
}