using NimbleCalculator.Core;

Console.WriteLine("Nimble Calculator!");

while (true) 
{
    Console.Write("\nEnter input: ");
    var input = Console.ReadLine();

    var calculator = new Calculator();
    (int result, string formula) = calculator.Add(input ?? string.Empty);

    Console.WriteLine($"Result: {result}");
    Console.WriteLine($"Formula: {formula}");
}
