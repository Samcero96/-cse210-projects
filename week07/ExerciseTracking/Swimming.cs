using System;

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int length, int laps)
        : base(date, length)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Each lap is 50 meters. Convert meters to kilometers, then kilometers to miles.
        return _laps * 50.0 / 1000 * 0.62;
    }

    public override double GetSpeed()
    {
        if (GetLength() <= 0)
        {
            return 0;
        }

        return (GetDistance() / GetLength()) * 60;
    }

    public override double GetPace()
    {
        if (GetDistance() <= 0)
        {
            return 0;
        }

        return GetLength() / GetDistance();
    }
}
