using System;
using System.Text;

public abstract class Goal
{
    private readonly string _name;
    private readonly string _description;
    private readonly int _points;

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract bool IsComplete { get; }

    // Each type of goal decides how an event affects completion and points.
    public abstract int RecordEvent();

    public string GetStatusMarker()
    {
        return IsComplete ? "[X]" : "[ ]";
    }

    public virtual string GetDetailsString()
    {
        return $"{GetStatusMarker()} {Name} ({Description})";
    }

    // Base64 protects names and descriptions containing commas, colons, or separators.
    protected static string Encode(string value)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    public abstract string GetStringRepresentation();
}
