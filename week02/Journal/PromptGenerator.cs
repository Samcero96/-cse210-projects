using System;
using System.Collections.Generic;

public class PromptGenerator
{
    public List<string> _prompts = new List<string>
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day?",
        "How did you see the hand of God in your life today?",
        "What is something you learned today?",
        "What was a challenge you faced today, and how did you handle it?",
        "What is one goal you want to work on tomorrow?",
        "What made you smile today?",
        "What is something you are grateful for today?"
    };

    private readonly Random _random = new Random();

    public string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }
}
