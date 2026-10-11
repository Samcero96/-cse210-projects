using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Store different kinds of shapes in one list using the base Shape type.
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("blue", 5));
        shapes.Add(new Rectangle("red", 4, 6));
        shapes.Add(new Circle("green", 5));

        // Polymorphism: each shape runs its own overridden GetArea method.
        foreach (Shape shape in shapes)
        {
            Console.WriteLine(
                $"The {shape.GetColor()} shape has an area of {shape.GetArea():F2}.");
        }
    }
}
