namespace Transports;

[Serializable]
public class Car : Transport
{
    private int _wheelsCount;

    public Car(string name, double averageSpeed, int wheelsCount) : base(name, averageSpeed)
    {
        WheelsCount = wheelsCount;
    }

    public int WheelsCount
    {
        get => _wheelsCount;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Количество колёс должно быть положительным");
            _wheelsCount = value;
        }
    }
}