using System;

public class Entry
{
    public string _date = "";
    public string _promptText = "";
    public string _entryText = "";

    public Entry()
    {
    }

    public Entry(string date, string promptText, string entryText)
    {
        _date = date;
        _promptText = promptText;
        _entryText = entryText;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine(_entryText);
        Console.WriteLine();
    }

    public string ToFileLine()
    {
        return $"{_date}\t{_promptText}\t{_entryText.Replace("\t", " ").Replace("\r", " ").Replace("\n", " ")}";
    }

    public static Entry FromFileLine(string line)
    {
        string[] parts = line.Split('\t');
        if (parts.Length < 3)
        {
            return new Entry();
        }

        return new Entry(parts[0], parts[1], string.Join("\t", parts, 2, parts.Length - 2));
    }
}
