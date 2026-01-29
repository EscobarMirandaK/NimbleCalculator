using System;
using System.Text.RegularExpressions;

namespace NimbleCalculator.Core
{
    public class Calculator
    {
        public (int, string) Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (0, "0 = 0");

            (List<string> delimiters, string numbersPart) = GetDelimiterAndInput(input);

            var parts = numbersPart.Split(delimiters.ToArray(), StringSplitOptions.None).ToList();
            var numbers = parts.Select(ParseNumber).ToList();

            if (numbers.Any(n => n < 0))
            {
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(", ", numbers.Where(n => n < 0).Select(n => n.ToString()))}");
            }

            var result = numbers.Sum();
            var formula = $"{string.Join(" + ", numbers)} = {result}";

            return (result, formula);
        }

        private static (List<string>, string) GetDelimiterAndInput(string input)
        {
            var delimiters = new List<string> { ",", "\n" };
            var numbersPart = input;

            if (input.StartsWith("//") && !input.StartsWith("//["))
            {
                var delimiterEndIndex = input.IndexOf('\n');
                if (delimiterEndIndex != -1)
                {
                    var customDelimiter = input.Substring(2, delimiterEndIndex - 2);
                    numbersPart = input.Substring(delimiterEndIndex + 1);
                    delimiters.Add(customDelimiter);
                }
            }

            if (input.StartsWith("//["))
            {
                var delimiterEndIndex = input.IndexOf("]\n");
                if (delimiterEndIndex != -1)
                {
                    var matches = Regex.Matches(input, @"\[(.*?)\]");

                    var customDelimiters = matches
                        .Select(m => m.Groups[1].Value)
                        .ToList();

                    delimiters.AddRange(customDelimiters);
                    numbersPart = input.Substring(delimiterEndIndex + 2);
                }
            }

            return (delimiters, numbersPart);
        }

        private int ParseNumber(string value)
        {
            if (int.TryParse(value, out var number))
            {
                return number > 1000 ? 0 : number;
            }

            return 0;
        }
    }
}
