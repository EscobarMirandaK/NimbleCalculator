using NimbleCalculator.Core;
// Allow the application to process entered entries until Ctrl+C is used 

Console.WriteLine("Nimble Calculator!");

// this was done before:
// Allow the application to process entered entries until Ctrl+C is used 
while (true) 
{
    Console.Write("\nEnter input: ");
    var input = Console.ReadLine();

    var calculator = new Calculator();
    (int result, string formula) = calculator.Add(input ?? string.Empty);

    Console.WriteLine($"Result: {result}");
    Console.WriteLine($"Formula: {formula}");
}
