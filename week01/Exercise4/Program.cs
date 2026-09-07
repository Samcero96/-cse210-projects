using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

int number = -1;

while (number != 0)
{
    Console.Write("Enter number: ");
    number = int.Parse(Console.ReadLine());

    if (number != 0)
    {
        numbers.Add(number);
    }
}

// Find the sum
int sum = 0;

foreach (int num in numbers)
{
    sum += num;
}

// Find the average
float average = (float)sum / numbers.Count;

// Find the largest number
int largest = numbers[0];

foreach (int num in numbers)
{
    if (num > largest)
    {
        largest = num;
    }
}

Console.WriteLine($"The sum is: {sum}");
Console.WriteLine($"The average is: {average}");
Console.WriteLine($"The largest number is: {largest}");
    }
}