namespace NimbleCalculator.Core
{
    public interface IOperation
    {
        string Symbol { get; }

        int Apply(IEnumerable<int> numbers);
    }
}
