
using Counter.Models;

namespace Counter.Views;

public partial class CounterPage : ContentPage, IQueryAttributable
{
    private CounterItem? _counter;

    public CounterPage()
    {
        InitializeComponent();
    }

    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Counter", out var item)
            && item is CounterItem counter)
        {
            _counter = counter;
            BindingContext = counter;
        }
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        if (_counter != null)
            _counter.Value++;
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        if (_counter != null)
            _counter.Value--;
    }
}
