using System;

namespace NimbleCalculator.Core
{
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var customDelimiter = ",";
            var numbersPart = input;

            if (input.StartsWith("//"))
            {
                var delimiterEndIndex = 3;
                if (delimiterEndIndex != -1)
                {
                    customDelimiter = input.Substring(2, delimiterEndIndex - 2);
                    numbersPart = input.Substring(delimiterEndIndex + 1);
                }
            }

            var parts = numbersPart.Split(new string[] { ",", "\n", customDelimiter }, StringSplitOptions.None).ToList();
            var numbers = parts.Select(ParseNumber).ToList();

            if (numbers.Any(n => n < 0))
            {
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(", ", numbers.Where(n => n < 0).Select(n => n.ToString()))}");
            }

            return numbers.Sum();
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
