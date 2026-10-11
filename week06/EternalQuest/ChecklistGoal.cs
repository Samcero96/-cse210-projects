using System;

public class ChecklistGoal : Goal
{
    private readonly int _targetCount;
    private readonly int _bonusPoints;
    private int _completedCount;

    public int TargetCount => _targetCount;
    public int BonusPoints => _bonusPoints;
    public int CompletedCount => _completedCount;
    public override bool IsComplete => _completedCount >= _targetCount;

    public ChecklistGoal(
        string name,
        string description,
        int points,
        int targetCount,
        int bonusPoints,
        int completedCount = 0)
        : base(name, description, points)
    {
        if (targetCount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(targetCount), "The target must be at least 1.");
        }

        if (bonusPoints < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bonusPoints), "Bonus points cannot be negative.");
        }

        if (completedCount < 0 || completedCount > targetCount)
        {
            throw new ArgumentOutOfRangeException(nameof(completedCount), "The completed count must be between 0 and the target.");
        }

        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _completedCount = completedCount;
    }

    public override int RecordEvent()
    {
        if (IsComplete)
        {
            return 0;
        }

        _completedCount++;
        int earnedPoints = Points;

        if (IsComplete)
        {
            earnedPoints += _bonusPoints;
        }

        return earnedPoints;
    }

    public override string GetDetailsString()
    {
        return $"{GetStatusMarker()} {Name} ({Description}) -- Completed {_completedCount}/{_targetCount} times";
    }

    public override string GetStringRepresentation()
    {
        return $"CHECKLIST|{Encode(Name)}|{Encode(Description)}|{Points}|{_targetCount}|{_bonusPoints}|{_completedCount}";
    }
}
