using CalculatorApp;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        // Set up dependency injection
        var serviceProvider = new ServiceCollection()
            .AddTransient<ICalculator, Calculator>()
            .BuildServiceProvider();

        var calculator = serviceProvider.GetService<ICalculator>();
        while (true) // Infinite loop to process input until Ctrl+C
        {
            Console.Write("Input: ");
            string input = Console.ReadLine();

            // Replace "\\n" with the actual newline character if necessary
            input = input.Replace("\\n", "\n");

            try
            {
                Console.WriteLine(calculator.Calculate(input));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
