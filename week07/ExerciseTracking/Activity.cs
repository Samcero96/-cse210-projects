using System;
using System.Globalization;

public abstract class Activity
{
    private DateTime _date;
    private int _length;

    public Activity(DateTime date, int length)
    {
        _date = date;
        _length = length;
    }

    public string GetDateString()
    {
        return _date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
    }

    public int GetLength()
    {
        return _length;
    }

    // Each derived activity calculates these values in its own way.
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{GetDateString()} {GetType().Name} ({_length} min): " +
               $"Distance {GetDistance():0.0} miles, " +
               $"Speed: {GetSpeed():0.0} mph, " +
               $"Pace: {GetPace():0.0} min per mile";
    }
}
