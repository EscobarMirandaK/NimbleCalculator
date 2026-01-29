namespace NimbleCalculator.Core
{
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var parts = input.Split(',','\n');
            var numbers = parts.Select(ParseNumber).ToList();

            if (numbers.Any(n => n < 0))
            {
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(", ", numbers.Where(n => n < 0).Select(n => n.ToString()))}");
            }

            return numbers.Sum();
        }

        private int ParseNumber(string value)
        {
            return int.TryParse(value, out var number) ? number : 0;
        }
    }
}
