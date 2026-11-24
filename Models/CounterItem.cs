
using System.ComponentModel;

namespace Counter.Models;

public class CounterItem : INotifyPropertyChanged
{
    public string Name { get; set; } = string.Empty;

    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
