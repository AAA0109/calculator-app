using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class Calculator : ICalculator
    {
        public string Calculate(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return "0 = 0";
            }

            var (numberSection, delimiters) = ParseInput(numbers);
            var tokens = Tokenize(numberSection, delimiters);
            var (parsedNumbers, formulaParts) = ProcessTokens(tokens);

            ValidateNumbers(parsedNumbers);

            int totalResult = CalculateResult(parsedNumbers);
            string formula = string.Join("+", formulaParts);

            return $"{formula} = {totalResult}";
        }

        private (string, List<string>) ParseInput(string numbers)
        {
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

            return (numberSection, delimiters);
        }

        private List<string> Tokenize(string numberSection, List<string> delimiters)
        {
            return numberSection.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private (List<int>, List<string>) ProcessTokens(List<string> tokens)
        {
            var parsedNumbers = new List<int>();
            var formulaParts = new List<string>();

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                // Try parsing each item and add valid integers to the list
                if (int.TryParse(token, out int parsedNumber))
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

            return (parsedNumbers, formulaParts);
        }

        private void ValidateNumbers(List<int> numbers)
        {
            var negativeNumbers = numbers.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new ArgumentException("Negative numbers are not allowed: " + string.Join(", ", negativeNumbers));
            }
        }

        private int CalculateResult(List<int> numbers)
        {
            return numbers.Sum();
        }
    }
}
