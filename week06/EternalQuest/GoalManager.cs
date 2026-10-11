using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private readonly List<Goal> _goals = new List<Goal>();
    private int _score;

    public int Score => _score;
    public IReadOnlyList<Goal> Goals => _goals.AsReadOnly();

    public void AddGoal(Goal goal)
    {
        if (goal == null)
        {
            throw new ArgumentNullException(nameof(goal));
        }

        _goals.Add(goal);
    }

    public int RecordEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= _goals.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(goalIndex));
        }

        Goal goal = _goals[goalIndex];
        if (goal.IsComplete)
        {
            return 0;
        }

        int earnedPoints = goal.RecordEvent();
        _score += earnedPoints;
        return earnedPoints;
    }

    public void SaveToFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("A file name is required.", nameof(fileName));
        }

        List<string> lines = new List<string> { $"SCORE|{_score}" };
        foreach (Goal goal in _goals)
        {
            lines.Add(goal.GetStringRepresentation());
        }

        File.WriteAllLines(fileName, lines);
    }

    public void LoadFromFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("A file name is required.", nameof(fileName));
        }

        string[] lines = File.ReadAllLines(fileName);
        List<Goal> loadedGoals = new List<Goal>();
        int loadedScore = 0;
        bool foundScore = false;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');
            switch (parts[0])
            {
                case "SCORE":
                    if (parts.Length != 2 || !int.TryParse(parts[1], out loadedScore) || loadedScore < 0)
                    {
                        throw new FormatException("The saved score line is invalid.");
                    }
                    foundScore = true;
                    break;

                case "SIMPLE":
                    if (parts.Length != 5)
                    {
                        throw new FormatException("A saved simple goal is invalid.");
                    }
                    loadedGoals.Add(new SimpleGoal(
                        Decode(parts[1]),
                        Decode(parts[2]),
                        ParseNonNegativeInt(parts[3]),
                        bool.Parse(parts[4])));
                    break;

                case "ETERNAL":
                    if (parts.Length != 4)
                    {
                        throw new FormatException("A saved eternal goal is invalid.");
                    }
                    loadedGoals.Add(new EternalGoal(
                        Decode(parts[1]),
                        Decode(parts[2]),
                        ParseNonNegativeInt(parts[3])));
                    break;

                case "CHECKLIST":
                    if (parts.Length != 7)
                    {
                        throw new FormatException("A saved checklist goal is invalid.");
                    }
                    loadedGoals.Add(new ChecklistGoal(
                        Decode(parts[1]),
                        Decode(parts[2]),
                        ParseNonNegativeInt(parts[3]),
                        ParsePositiveInt(parts[4]),
                        ParseNonNegativeInt(parts[5]),
                        ParseNonNegativeInt(parts[6])));
                    break;

                default:
                    throw new FormatException($"Unknown saved data type: {parts[0]}");
            }
        }

        if (!foundScore)
        {
            throw new FormatException("The save file does not contain a score.");
        }

        // Replace the current data only after the entire save file was read successfully.
        _goals.Clear();
        _goals.AddRange(loadedGoals);
        _score = loadedScore;
    }

    private static string Decode(string value)
    {
        try
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        catch (FormatException exception)
        {
            throw new FormatException("A saved name or description is invalid.", exception);
        }
    }

    private static int ParseNonNegativeInt(string value)
    {
        if (!int.TryParse(value, out int result) || result < 0)
        {
            throw new FormatException("A saved number must be zero or greater.");
        }

        return result;
    }

    private static int ParsePositiveInt(string value)
    {
        if (!int.TryParse(value, out int result) || result < 1)
        {
            throw new FormatException("A saved target must be at least 1.");
        }

        return result;
    }
}
