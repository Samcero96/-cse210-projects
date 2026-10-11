using System;
using System.Collections.Generic;

class Program
{
    private const string DefaultFileName = "eternalQuest.txt";

    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool keepRunning = true;

        while (keepRunning)
        {
            DisplayDashboard(manager.Score);
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Create new goal");
            Console.WriteLine("  2. List goals");
            Console.WriteLine("  3. Record goal event");
            Console.WriteLine("  4. Save goals");
            Console.WriteLine("  5. Load goals");
            Console.WriteLine("  6. Quit");

            int choice = ReadInt("Select an option: ", 1, 6);
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal(manager);
                    break;
                case 2:
                    ListGoals(manager.Goals);
                    break;
                case 3:
                    RecordGoalEvent(manager);
                    break;
                case 4:
                    SaveGoals(manager);
                    break;
                case 5:
                    LoadGoals(manager);
                    break;
                case 6:
                    Console.WriteLine("Keep going. Every good choice moves your quest forward!");
                    keepRunning = false;
                    break;
            }
        }
    }

    private static void DisplayDashboard(int score)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("          ETERNAL QUEST              ");
        Console.WriteLine("====================================");
        Console.WriteLine($"Total score: {score}");

        // Creativity beyond the core requirements: the player's level and title increase
        // as their score grows. This gives the user a longer-term reward to work toward.
        int level = (score / 500) + 1;
        Console.WriteLine($"Quest level: {level} - {GetRank(score)}");
        Console.WriteLine("Earn 500 points to reach the next level!");
    }

    private static string GetRank(int score)
    {
        if (score < 500) return "Quest Beginner";
        if (score < 1000) return "Faithful Seeker";
        if (score < 2000) return "Steady Builder";
        if (score < 3500) return "Quest Champion";
        return "Eternal Quest Hero";
    }

    private static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("What kind of goal would you like to create?");
        Console.WriteLine("  1. Simple goal (completed once)");
        Console.WriteLine("  2. Eternal goal (can be recorded repeatedly)");
        Console.WriteLine("  3. Checklist goal (completed a set number of times)");
        int type = ReadInt("Choose a goal type: ", 1, 3);

        string name = ReadRequiredText("Goal name: ");
        string description = ReadRequiredText("Goal description: ");
        int points = ReadInt("Points earned for each event: ", 1);

        switch (type)
        {
            case 1:
                manager.AddGoal(new SimpleGoal(name, description, points));
                break;
            case 2:
                manager.AddGoal(new EternalGoal(name, description, points));
                break;
            case 3:
                int target = ReadInt("How many times must you complete it? ", 1);
                int bonus = ReadInt("Bonus points for reaching the target: ", 0);
                manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                break;
        }

        Console.WriteLine("Goal created successfully!");
    }

    private static void ListGoals(IReadOnlyList<Goal> goals)
    {
        Console.WriteLine("Your goals:");
        if (goals.Count == 0)
        {
            Console.WriteLine("You haven't created any goals yet.");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
        }
    }

    private static void RecordGoalEvent(GoalManager manager)
    {
        if (manager.Goals.Count == 0)
        {
            Console.WriteLine("There are no goals to record yet. Create a goal first.");
            return;
        }

        ListGoals(manager.Goals);
        int selectedNumber = ReadInt("Which goal did you accomplish? ", 1, manager.Goals.Count);
        Goal selectedGoal = manager.Goals[selectedNumber - 1];

        if (selectedGoal.IsComplete)
        {
            Console.WriteLine("That goal is already complete. Choose a goal that still needs progress.");
            return;
        }

        int earned = manager.RecordEvent(selectedNumber - 1);
        Console.WriteLine($"You earned {earned} points!");

        if (selectedGoal is ChecklistGoal checklist && checklist.IsComplete)
        {
            Console.WriteLine($"Checklist goal completed! Your {checklist.BonusPoints}-point bonus was included.");
        }

        Console.WriteLine($"Your new total is {manager.Score} points.");
    }

    private static void SaveGoals(GoalManager manager)
    {
        string fileName = ReadFileName("Save to which file? (Press Enter for eternalQuest.txt): ");
        try
        {
            manager.SaveToFile(fileName);
            Console.WriteLine($"Your goals and score have been saved to '{fileName}'.");
        }
        catch (Exception exception) when (exception is System.IO.IOException || exception is UnauthorizedAccessException || exception is ArgumentException)
        {
            Console.WriteLine($"Could not save the file: {exception.Message}");
        }
    }

    private static void LoadGoals(GoalManager manager)
    {
        string fileName = ReadFileName("Load from which file? (Press Enter for eternalQuest.txt): ");
        try
        {
            manager.LoadFromFile(fileName);
            Console.WriteLine($"Your goals and score have been loaded from '{fileName}'.");
        }
        catch (Exception exception) when (exception is System.IO.IOException || exception is UnauthorizedAccessException || exception is ArgumentException || exception is FormatException)
        {
            Console.WriteLine($"Could not load the file: {exception.Message}");
        }
    }

    private static string ReadFileName(string prompt)
    {
        Console.Write(prompt);
        string fileName = Console.ReadLine() ?? "";
        return string.IsNullOrWhiteSpace(fileName) ? DefaultFileName : fileName.Trim();
    }

    private static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("Please enter some text.");
        }
    }

    private static int ReadInt(string prompt, int minimum, int? maximum = null)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int value) && value >= minimum && (!maximum.HasValue || value <= maximum.Value))
            {
                return value;
            }

            if (maximum.HasValue)
            {
                Console.WriteLine($"Please enter a whole number from {minimum} to {maximum.Value}.");
            }
            else
            {
                Console.WriteLine($"Please enter a whole number that is at least {minimum}.");
            }
        }
    }
}
