public class EternalGoal : Goal
{
    public override bool IsComplete => false;

    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        // Eternal goals can be recorded repeatedly and never become complete.
        return Points;
    }

    public override string GetStringRepresentation()
    {
        return $"ETERNAL|{Encode(Name)}|{Encode(Description)}|{Points}";
    }
}
