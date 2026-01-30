namespace NimbleCalculator.Core
{
    public class Multiply : IOperation
    {
        public string Symbol => OperationType.Multiplication;

        public int Apply(IEnumerable<int> numbers)
        {
            var list = numbers.ToList();
            if (!list.Any()) return 0;

            return list.Aggregate(1, (acc, n) => acc * n);
        }
    }

}
