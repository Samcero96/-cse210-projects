using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

/// <summary>
/// Gives the user a prompt and counts the items they can list before time runs out.
/// </summary>
public class ListingActivity : Activity
{
    private readonly string[] _prompts;
    private readonly Random _random;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you notice the good things in your life. List as many responses as you can to the prompt before the time ends.")
    {
        _prompts = new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths you have?",
            "Who have you helped this week?",
            "When have you felt the Holy Ghost this month?",
            "What are things you are grateful for?",
            "What are ways you can make someone's day better?"
        };

        _random = new Random();
    }

    protected override void PerformActivity()
    {
        string prompt = _prompts[_random.Next(_prompts.Length)];
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();
        Console.WriteLine("Press Enter after each item. Keep going until the time is up.");

        int itemCount = 0;
        Stopwatch timer = Stopwatch.StartNew();
        StringBuilder currentItem = new StringBuilder();

        // The key polling loop lets the activity stop when time runs out, even if the user
        // is not pressing keys. A submitted item is counted when the user presses Enter.
        if (!Console.IsInputRedirected)
        {
            Console.Write("Item 1: ");
            while (timer.Elapsed.TotalSeconds < Duration)
            {
                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    if (!string.IsNullOrWhiteSpace(currentItem.ToString()))
                    {
                        itemCount++;
                    }

                    currentItem.Clear();
                    if (timer.Elapsed.TotalSeconds < Duration)
                    {
                        Console.Write($"Item {itemCount + 1}: ");
                    }
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (currentItem.Length > 0)
                    {
                        currentItem.Length--;
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    currentItem.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }

            Console.WriteLine();
        }
        else
        {
            // Fallback for redirected input, such as running the program with piped input.
            while (timer.Elapsed.TotalSeconds < Duration)
            {
                string? item = Console.ReadLine();
                if (item is null)
                {
                    break;
                }

                if (!string.IsNullOrWhiteSpace(item) && timer.Elapsed.TotalSeconds <= Duration)
                {
                    itemCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {itemCount} item(s)!");
    }
}
