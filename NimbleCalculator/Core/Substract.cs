namespace NimbleCalculator.Core
{
    public class Substract : IOperation
    {
        public string Symbol => OperationType.Substraction;

        public int Apply(IEnumerable<int> numbers)
        {
            var list = numbers.ToList();
            if (!list.Any()) return 0;

            return list.Skip(1).Aggregate(list.First(), (acc, n) => acc - n);
        }
    }


}
