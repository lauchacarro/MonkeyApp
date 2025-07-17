using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// Provides static helper methods for managing monkey data.
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> monkeys;
    private static readonly Dictionary<string, int> accessCounts;
    private static readonly object lockObj = new();

    static MonkeyHelper()
    {
        monkeys = new List<Monkey>
        {
            new Monkey { Name = "Baboon", Location = "Africa & Asia", Population = 10000 },
            new Monkey { Name = "Capuchin Monkey", Location = "Central & South America", Population = 23000 },
            new Monkey { Name = "Blue Monkey", Location = "Central and East Africa", Population = 12000 },
            new Monkey { Name = "Squirrel Monkey", Location = "Central & South America", Population = 11000 },
            new Monkey { Name = "Golden Lion Tamarin", Location = "Brazil", Population = 19000 },
            new Monkey { Name = "Howler Monkey", Location = "South America", Population = 8000 },
            new Monkey { Name = "Japanese Macaque", Location = "Japan", Population = 1000 },
            new Monkey { Name = "Mandrill", Location = "Southern Cameroon, Gabon, and Congo", Population = 17000 },
            new Monkey { Name = "Proboscis Monkey", Location = "Borneo", Population = 15000 },
            new Monkey { Name = "Sebastian", Location = "Seattle", Population = 1 },
            new Monkey { Name = "Henry", Location = "Phoenix", Population = 1 },
            new Monkey { Name = "Red-shanked douc", Location = "Vietnam", Population = 1300 },
            new Monkey { Name = "Mooch", Location = "Seattle", Population = 1 }
        };
        accessCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets all monkeys.
    /// </summary>
    /// <returns>A list of all monkeys.</returns>
    public static IReadOnlyList<Monkey> GetMonkeys()
    {
        return monkeys.AsReadOnly();
    }

    /// <summary>
    /// Gets a random monkey.
    /// </summary>
    /// <returns>A random monkey.</returns>
    public static Monkey GetRandomMonkey()
    {
        var random = new Random();
        var monkey = monkeys[random.Next(monkeys.Count)];
        TrackAccess(monkey.Name);
        return monkey;
    }

    /// <summary>
    /// Finds a monkey by name.
    /// </summary>
    /// <param name="name">The name of the monkey.</param>
    /// <returns>The monkey if found; otherwise, null.</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        var monkey = monkeys.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
        if (monkey != null)
        {
            TrackAccess(monkey.Name);
        }
        return monkey;
    }

    /// <summary>
    /// Gets the access count for a monkey by name.
    /// </summary>
    /// <param name="name">The name of the monkey.</param>
    /// <returns>The access count.</returns>
    public static int GetAccessCount(string name)
    {
        lock (lockObj)
        {
            return accessCounts.TryGetValue(name, out var count) ? count : 0;
        }
    }

    private static void TrackAccess(string name)
    {
        lock (lockObj)
        {
            if (accessCounts.ContainsKey(name))
            {
                accessCounts[name]++;
            }
            else
            {
                accessCounts[name] = 1;
            }
        }
    }
}
