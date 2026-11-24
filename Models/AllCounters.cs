
using System.Collections.ObjectModel;

namespace Counter.Models;

public class AllCounters
{
    public ObservableCollection<CounterItem> Counters { get; } = new();
    private string _filePath =
        Path.Combine(FileSystem.AppDataDirectory, "counters.txt");

    public AllCounters()
    {
        LoadCounters();
    }
    public void AddCounter(string name)
    {
        Counters.Add(new CounterItem
        {
            Name = name,
            Value = 0
        });
    }

    public void SaveCounters()
    {
        List<string> lines = new();

        foreach (var counter in Counters)
        {
            lines.Add($"{counter.Name};{counter.Value}");
        }

        File.WriteAllLines(_filePath, lines);
    }

    public void LoadCounters()
    {
        if (File.Exists(_filePath))
        {
            string[] lines = File.ReadAllLines(_filePath);

            Counters.Clear();

            foreach (string line in lines)
            {
                int separator = line.LastIndexOf(';');

                if (separator < 0)
                    continue;

                string name = line.Substring(0, separator);
                string valueText = line.Substring(separator + 1);

                if (int.TryParse(valueText, out int value))
                {
                    Counters.Add(new CounterItem
                    {
                        Name = name,
                        Value = value
                    });
                }
            }
        }
    }
}
