using System;
using System.Threading;

/// <summary>
/// Base class containing the information and behavior shared by all mindfulness activities.
/// </summary>
public abstract class Activity
{
    private readonly string _activityName;
    private readonly string _description;
    private int _duration;

    protected Activity(string activityName, string description)
    {
        _activityName = activityName;
        _description = description;
        _duration = 0;
    }

    public string ActivityName => _activityName;

    public string Description => _description;

    public int Duration => _duration;

    /// <summary>
    /// Runs the common activity setup, the activity itself, and the common ending message.
    /// </summary>
    public void Run()
    {
        DisplayStartingMessage();
        AskForDuration();

        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
        Console.WriteLine();

        PerformActivity();
        DisplayEndingMessage();
    }

    protected abstract void PerformActivity();

    private void DisplayStartingMessage()
    {
        Console.WriteLine();
        Console.WriteLine($"Welcome to the {_activityName}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
    }

    private void AskForDuration()
    {
        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                _duration = seconds;
                return;
            }

            Console.WriteLine("Please enter a whole number greater than zero.");
        }
    }

    private void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(2);
        Console.WriteLine();
        Console.WriteLine($"You have completed {_duration} seconds of the {_activityName}.");
        ShowSpinner(3);
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a spinner animation using backspaces to update the same console position.
    /// </summary>
    protected void ShowSpinner(int seconds)
    {
        string[] animation = { "|", "/", "-", "\\" };
        int frames = seconds * 4;

        for (int index = 0; index < frames; index++)
        {
            Console.Write(animation[index % animation.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
        }
    }

    /// <summary>
    /// Displays a countdown animation using backspaces to clear each number.
    /// </summary>
    protected void ShowCountdown(int seconds)
    {
        for (int number = seconds; number > 0; number--)
        {
            Console.Write(number);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}
