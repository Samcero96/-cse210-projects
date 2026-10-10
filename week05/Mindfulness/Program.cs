using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. View Activity History");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");

            string? choice = Console.ReadLine();
            Activity? activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectingActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    ViewActivityHistory();
                    break;
                case "5":
                    isRunning = false;
                    Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Please select 1, 2, 3, 4, or 5.");
                    break;
            }

            if (activity is not null)
            {
                activity.Run();
                SaveActivityHistory(activity);
            }
        }
    }

    // Creativity feature: save each completed activity to a history file and let the user view it
    // from the menu. The log records the date/time, activity name, and requested duration.
    private static void SaveActivityHistory(Activity activity)
    {
        string historyFile = Path.Combine(Environment.CurrentDirectory, "activity-history.txt");
        string historyEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {activity.ActivityName} | {activity.Duration} seconds{Environment.NewLine}";

        try
        {
            File.AppendAllText(historyFile, historyEntry);
            Console.WriteLine("Activity saved to your history.");
        }
        catch (IOException)
        {
            Console.WriteLine("Your activity finished, but the history file could not be saved.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Your activity finished, but permission to save the history file was denied.");
        }
    }

    private static void ViewActivityHistory()
    {
        string historyFile = Path.Combine(Environment.CurrentDirectory, "activity-history.txt");

        Console.WriteLine();
        Console.WriteLine("Activity History");
        Console.WriteLine("----------------");

        try
        {
            if (File.Exists(historyFile))
            {
                string history = File.ReadAllText(historyFile);
                if (string.IsNullOrWhiteSpace(history))
                {
                    Console.WriteLine("No activities have been recorded yet.");
                }
                else
                {
                    Console.WriteLine(history);
                }
            }
            else
            {
                Console.WriteLine("No activity history found yet. Complete an activity to create one.");
            }
        }
        catch (IOException)
        {
            Console.WriteLine("The activity history could not be read.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Permission to read the activity history was denied.");
        }
    }
}
