using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        /*
         * Creativity feature:
         * Instead of using only one scripture, this program contains
         * a small library of scriptures and randomly selects one
         * for the user to practice memorizing.
         */

        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son"
            ),

            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding"
            ),

            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me"
            )
        };

        int randomIndex = Random.Shared.Next(scriptures.Count);
        Scripture scripture = scriptures[randomIndex];

        Console.Clear();

        while (true)
        {
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            // End when every word is hidden.
            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            Console.Write("Press Enter to hide words or type 'quit': ");
            string input = Console.ReadLine() ?? "";

            // End if the user types quit.
            if (input.Trim().ToLower() == "quit")
            {
                break;
            }

            // Hide three random words.
            scripture.HideRandomWords(3);

            // Clear the screen before showing the updated scripture.
            Console.Clear();
        }

        Console.WriteLine();

        if (scripture.IsCompletelyHidden())
        {
            Console.WriteLine("Great job! The scripture is completely hidden.");
        }
        else
        {
            Console.WriteLine("Program ended.");
        }
    }
}