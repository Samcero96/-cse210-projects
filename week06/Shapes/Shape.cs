public class Shape
{
    private string _color = "";

    public Shape(string color)
    {
        _color = color;
    }

    public string GetColor()
    {
        return _color;
    }

    public void SetColor(string color)
    {
        _color = color;
    }

    // Each derived shape provides its own area calculation.
    public virtual double GetArea()
    {
        return 0;
    }
}
