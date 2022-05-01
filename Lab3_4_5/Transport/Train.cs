namespace Transports;

[Serializable]
public class Train : Transport
{
    private int _wagonsCount;

    public Train(string name, double averageSpeed, int wagonsCount) : base(name, averageSpeed)
    {
        WagonsCount = wagonsCount;
    }

    public int WagonsCount
    {
        get => _wagonsCount;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Количество вагонов должно быть положительным");
            _wagonsCount = value;
        }
    }
}