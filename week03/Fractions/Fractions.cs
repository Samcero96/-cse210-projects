public class Fraction
{
    private int _top;
    private int _bottom;

    // No-parameter constructor: 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // One-parameter constructor: top/1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // Two-parameter constructor: top/bottom
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getter for top
    public int GetTop()
    {
        return _top;
    }

    // Setter for top
    public void SetTop(int top)
    {
        _top = top;
    }

    // Getter for bottom
    public int GetBottom()
    {
        return _bottom;
    }

    // Setter for bottom
    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Returns the fraction as a string
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Returns the decimal value
    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }
}