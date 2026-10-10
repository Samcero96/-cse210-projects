using System;
using System.Diagnostics;

/// <summary>
/// Helps the user reflect on meaningful experiences by considering prompts and questions.
/// </summary>
public class ReflectingActivity : Activity
{
    private readonly string[] _prompts;
    private readonly string[] _questions;
    private readonly Random _random;

    public ReflectingActivity()
        : base(
            "Reflecting Activity",
            "This activity will help you reflect on times when you have shown strength, kindness, or resilience. Think deeply about the prompts and questions.")
    {
        _prompts = new string[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you showed kindness without expecting anything in return.",
            "Think of a time when you learned something important from a challenge."
        };

        _questions = new string[]
        {
            "Why was this experience meaningful to you?",
            "How did you feel when it was happening?",
            "What did you learn about yourself?",
            "What strength did you use in that situation?",
            "How did your actions help another person?",
            "How could you use what you learned in the future?",
            "What would you do differently if you faced the same situation again?",
            "How can you remember this experience when life becomes difficult?"
        };

        _random = new Random();
    }

    protected override void PerformActivity()
    {
        string prompt = _prompts[_random.Next(_prompts.Length)];

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();
        Console.Write("When you have something in mind, take a moment to begin thinking about it...");
        ShowSpinner(3);
        Console.WriteLine();

        // Start the selected reflection time after the introductory preparation pause.
        Stopwatch timer = Stopwatch.StartNew();
        while (timer.Elapsed.TotalSeconds < Duration)
        {
            string question = _questions[_random.Next(_questions.Length)];
            Console.WriteLine();
            Console.WriteLine($"> {question}");

            int secondsLeft = Math.Max(1, (int)Math.Ceiling(Duration - timer.Elapsed.TotalSeconds));
            ShowSpinner(Math.Min(5, secondsLeft));

            if (timer.Elapsed.TotalSeconds < Duration)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
    }
}
