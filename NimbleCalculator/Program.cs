using NimbleCalculator.Core;

Console.WriteLine("Nimble Calculator!");

while (true) 
{
    Console.Write("Enter input: ");
    var input = Console.ReadLine();

    var calculator = new Calculator();
    var result = calculator.Add(input ?? string.Empty);

    Console.WriteLine($"Result: {result}");
}
