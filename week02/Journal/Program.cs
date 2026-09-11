using System;

class Program
{
    static void Main(string[] args)
    {
        Journal theJournal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        // Creativity beyond the core requirements:
        // 1. The program uses eight varied prompts, including a faith-centered prompt.
        // 2. The menu validates choices instead of crashing on invalid input.
        // 3. Save/load uses a simple tab-separated format so journal entries persist between runs.
        // 4. The program gives a friendly message when the journal is empty or a file is missing.

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine() ?? "";
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    theJournal.AddEntry(new Entry(date, prompt, response));
                    Console.WriteLine("Entry added.");
                    break;

                case "2":
                    theJournal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter a file name: ");
                    string saveFile = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(saveFile))
                    {
                        Console.WriteLine("Please enter a valid file name.");
                    }
                    else
                    {
                        theJournal.SaveToFile(saveFile);
                        Console.WriteLine("Journal saved successfully.");
                    }
                    break;

                case "4":
                    Console.Write("Enter a file name: ");
                    string loadFile = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(loadFile))
                    {
                        Console.WriteLine("Please enter a valid file name.");
                    }
                    else
                    {
                        theJournal.LoadFromFile(loadFile);
                        Console.WriteLine("Journal loaded successfully.");
                    }
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1-5.");
                    break;
            }
        }
    }
}
