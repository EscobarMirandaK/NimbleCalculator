using System.Text.RegularExpressions;

namespace NimbleCalculator.Core
{
    public class Calculator
    {
        private readonly CalculatorConfig config;
        private readonly IDictionary<string, IOperation> operations;

        public Calculator(CalculatorConfig config, IEnumerable<IOperation> operations)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.operations = operations.ToDictionary(o => o.Symbol);
        }

        public Calculator() : this(new CalculatorConfig(), new IOperation[] { new Sum(), new Substract(), new Divide(), new Multiply() })
        {
        }

        public (int result, string formula) Calculate(string input, string operationSymbol = OperationType.Addition)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (0, "0 = 0");

            if (!this.operations.TryGetValue(operationSymbol, out var operation))
                throw new InvalidOperationException($"Unsupported operation '{operationSymbol}'.");

            var (delimiters, numbersPart) = GetDelimiterAndInput(input);

            var numbers = numbersPart
                .Split(delimiters.ToArray(), StringSplitOptions.None)
                .Select(ParseNumber)
                .ToList();

            ValidateNegatives(numbers);

            var result = operation.Apply(numbers);
            var formula = $"{string.Join($" {operation.Symbol} ", numbers)} = {result}";

            return (result, formula);
        }

        private (List<string> delimiters, string numbersPart) GetDelimiterAndInput(string input)
        {
            var delimiters = new List<string> { ",", "\n" };
            var numbersPart = input;

            if (!string.IsNullOrEmpty(this.config.AlternateDelimiter))
                delimiters.Add(this.config.AlternateDelimiter);

            if (input.StartsWith("//") && !input.StartsWith("//["))
            {
                var endIndex = input.IndexOf('\n');
                if (endIndex > -1)
                {
                    delimiters.Add(input.Substring(2, endIndex - 2));
                    numbersPart = input[(endIndex + 1)..];
                }
            }
            else if (input.StartsWith("//["))
            {
                var endIndex = input.IndexOf("]\n");
                if (endIndex > -1)
                {
                    var matches = Regex.Matches(input, @"\[(.*?)\]");
                    delimiters.AddRange(matches.Select(m => m.Groups[1].Value));
                    numbersPart = input[(endIndex + 2)..];
                }
            }

            return (delimiters, numbersPart);
        }

        private int ParseNumber(string value)
        {
            if (!int.TryParse(value, out var number))
                return 0;

            return number > this.config.MaxValue ? 0 : number;
        }

        private void ValidateNegatives(IEnumerable<int> numbers)
        {
            if (!this.config.DenyNegatives)
                return;

            var negatives = numbers.Where(n => n < 0).ToList();
            if (negatives.Any())
                throw new ArgumentException(
                    $"Negative numbers are not allowed: {string.Join(", ", negatives)}");
        }
    }
}