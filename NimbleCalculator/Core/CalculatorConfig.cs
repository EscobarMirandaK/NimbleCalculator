namespace NimbleCalculator.Core
{
    public class CalculatorConfig
    {
        public string? AlternateDelimiter { get; set; }
        public bool DenyNegatives { get; set; } = true;
        public int MaxValue { get; set; } = 1000;
    }
}
