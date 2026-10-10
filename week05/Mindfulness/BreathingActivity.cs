using System;
using System.Diagnostics;

/// <summary>
/// Guides the user through alternating breathing in and breathing out.
/// </summary>
public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by guiding you to breathe in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        Stopwatch timer = Stopwatch.StartNew();

        while (timer.Elapsed.TotalSeconds < Duration)
        {
            DisplayBreathingPhase("Breathe in...", timer);

            if (timer.Elapsed.TotalSeconds >= Duration)
            {
                break;
            }

            DisplayBreathingPhase("Breathe out...", timer);
        }

        Console.WriteLine();
    }

    private void DisplayBreathingPhase(string message, Stopwatch timer)
    {
        Console.Write($"{message} ");

        // Each breathing phase counts down from four, stopping when the requested session ends.
        for (int count = 4; count >= 1; count--)
        {
            if (timer.Elapsed.TotalSeconds >= Duration)
            {
                break;
            }

            Console.Write(count);
            System.Threading.Thread.Sleep(1000);
            Console.Write("\b \b");
        }

        Console.WriteLine();
    }
}
