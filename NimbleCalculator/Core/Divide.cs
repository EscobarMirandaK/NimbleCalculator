namespace NimbleCalculator.Core
{
    public class Divide : IOperation
    {
        public string Symbol => OperationType.Division;

        public int Apply(IEnumerable<int> numbers)
        {
            var list = numbers.ToList();
            if (!list.Any()) return 0;

            return list.Skip(1).Aggregate(list.First(), (acc, n) =>
            {
                if (n == 0)
                    throw new DivideByZeroException();

                return acc / n;
            });
        }
    }

}
