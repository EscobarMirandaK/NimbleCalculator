namespace NimbleCalculator.Core
{
    public class Sum : IOperation
    {
        public string Symbol => OperationType.Addition;

        public int Apply(IEnumerable<int> numbers)
            => numbers.Sum();
    }

}
