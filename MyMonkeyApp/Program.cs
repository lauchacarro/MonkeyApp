
using MyMonkeyApp;

namespace MyMonkeyApp;

internal class Program
{
    private const string MonkeyAsciiArt = @"
         .=""=.
        ( o o )
        /  V  \
       /(  _  )\
         ^^ ^^ ";

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Monkey App ===");
            Console.WriteLine("1. List all monkeys");
            Console.WriteLine("2. Get details for a specific monkey by name");
            Console.WriteLine("3. Get a random monkey");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    ListAllMonkeys();
                    break;
                case "2":
                    GetMonkeyByName();
                    break;
                case "3":
                    GetRandomMonkey();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListAllMonkeys()
    {
        Console.Clear();
        Console.WriteLine("All Monkeys:");
        var monkeys = MonkeyHelper.GetMonkeys();
        foreach (var monkey in monkeys)
        {
            var count = MonkeyHelper.GetAccessCount(monkey.Name);
            Console.WriteLine($"- {monkey.Name} (Location: {monkey.Location}, Population: {monkey.Population}, Accessed: {count} times)");
        }
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    private static void GetMonkeyByName()
    {
        Console.Clear();
        Console.Write("Enter monkey name: ");
        var name = Console.ReadLine();
        var monkey = MonkeyHelper.GetMonkeyByName(name ?? string.Empty);
        if (monkey != null)
        {
            ShowMonkeyDetails(monkey);
        }
        else
        {
            Console.WriteLine("Monkey not found.");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    private static void GetRandomMonkey()
    {
        Console.Clear();
        var monkey = MonkeyHelper.GetRandomMonkey();
        ShowMonkeyDetails(monkey);
    }

    private static void ShowMonkeyDetails(Monkey monkey)
    {
        Console.WriteLine(MonkeyAsciiArt);
        Console.WriteLine($"\nName: {monkey.Name}");
        Console.WriteLine($"Location: {monkey.Location}");
        Console.WriteLine($"Population: {monkey.Population}");
        var count = MonkeyHelper.GetAccessCount(monkey.Name);
        Console.WriteLine($"Accessed: {count} times");
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
