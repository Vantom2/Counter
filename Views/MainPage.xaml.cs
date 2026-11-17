
using System.Collections.ObjectModel;
using Counter.Models;

namespace Counter.Views;

public partial class MainPage : ContentPage
{
    public ObservableCollection<CounterItem> Counters { get; } = new();

    private string _filePath =
        Path.Combine(FileSystem.AppDataDirectory, "counters.txt");

    public MainPage()
    {
        InitializeComponent();

        BindingContext = this;

        LoadCounters();
    }

   private void AddCounterClicked(object sender, EventArgs e)
{
    var name = NameEntry.Text?.Trim();

    if (string.IsNullOrEmpty(name))
        return;

    Counters.Add(new CounterItem
    {
        Name = name,
        Value = 0
    });

    NameEntry.Text = string.Empty;
}

    private void PlusClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is CounterItem counter)
        {
            int index = Counters.IndexOf(counter);

            if (index >= 0)
            {
                Counters[index] = new CounterItem
                {
                    Name = counter.Name,
                    Value = counter.Value + 1
                };
            }
        }
    }

    private void MinusClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is CounterItem counter)
        {
            int index = Counters.IndexOf(counter);

            if (index >= 0)
            {
                
                Counters[index] = new CounterItem
                {
                    Name = counter.Name,
                    Value = counter.Value - 1
                };
            }
        }
    }

  
    private async void SaveClicked(object sender, EventArgs e)
    {
        try
        {
            List<string> lines = new();

            foreach (var counter in Counters)
            {
                
                lines.Add($"{counter.Name};{counter.Value}");
            }

            
            File.WriteAllLines(_filePath, lines);

            await DisplayAlertAsync(
                "sukces",
                "liczniki zostały zapisane",
                "zamknij");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "blad",
                $"nie udało sie zapisac: {ex.Message}",
                "zamknij");
        }
    }

    private void LoadCounters()
    {
        try
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
        catch (Exception ex)
        {
            _ = DisplayAlertAsync(
                "bład",
                $"nie udało sie wczytać danych: {ex.Message}",
                "zamknij");
        }
    }
}
