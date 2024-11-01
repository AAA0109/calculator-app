﻿using System;
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

        char customDelimiter = ',';
        string numberSection = numbers;

        // Check for custom delimiter syntax
        if (numbers.StartsWith("//") && numbers.Length > 4 && numbers[3] == '\n')
        {
            customDelimiter = numbers[2];
            numberSection = numbers.Substring(4); // Remove the custom delimiter part from the input
        }

        // Split by custom delimiter, comma, or newline characters
        var delimiters = new[] { customDelimiter, '\n' };
        var numberArray = numberSection.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

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