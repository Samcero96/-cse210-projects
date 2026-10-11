public class SimpleGoal : Goal
{
    private bool _isComplete;

    public override bool IsComplete => _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            return 0;
        }

        _isComplete = true;
        return Points;
    }

    public override string GetStringRepresentation()
    {
        return $"SIMPLE|{Encode(Name)}|{Encode(Description)}|{Points}|{_isComplete}";
    }
}
