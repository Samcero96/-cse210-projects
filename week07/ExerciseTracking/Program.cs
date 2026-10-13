using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // The list uses the base type, Activity, but stores all three derived activity types.
        // Calling GetSummary() on each item demonstrates polymorphism through the shared interface.
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 9.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 20));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
