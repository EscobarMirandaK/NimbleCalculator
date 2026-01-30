using Microsoft.Extensions.DependencyInjection;
using NimbleCalculator.Core;

Console.WriteLine("Nimble Calculator!");

var config = ParseArguments(args);

var services = new ServiceCollection();

services.AddSingleton(config);
services.AddTransient<Calculator>();
services.AddTransient<IOperation, Sum>();
services.AddTransient<IOperation, Substract>();
services.AddTransient<IOperation, Multiply>();
services.AddTransient<IOperation, Divide>();

var serviceProvider = services.BuildServiceProvider();

var calculator = serviceProvider.GetRequiredService<Calculator>();

while (true)
{
    Console.Write("\nEnter input: ");
    var input = Console.ReadLine() ?? string.Empty;

    try
    {
        (int result, string formula) = calculator.Calculate(input);

        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Formula: {formula}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static CalculatorConfig ParseArguments(string[] args)
{
    var config = new CalculatorConfig
    {
        AlternateDelimiter = null,
        DenyNegatives = true,
        MaxValue = 1000
    };

    foreach (var arg in args)
    {
        if (arg.StartsWith("--delimiter="))
            config.AlternateDelimiter = arg.Split('=', 2)[1];

        else if (arg == "--allow-negatives")
            config.DenyNegatives = false;

        else if (arg.StartsWith("--max=") &&
                 int.TryParse(arg.Split('=', 2)[1], out var max))
            config.MaxValue = max;
    }

    return config;
}
