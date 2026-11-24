
using Counter.Models;

namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    private AllCounters _allCounters = new();

    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = _allCounters;
    }

    private void OnAddCounterClicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name))
            return;

        _allCounters.AddCounter(name);

        NameEntry.Text = string.Empty;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        _allCounters.SaveCounters();
    }
    private async void OnCounterSelected(
        object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        if (e.CurrentSelection[0] is CounterItem counter)
        {
            ((CollectionView)sender).SelectedItem = null;

            await Shell.Current.GoToAsync(
                nameof(CounterPage),
                new Dictionary<string, object>
                {
                    ["Counter"] = counter
                });
        }
    }
}
