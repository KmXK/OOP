namespace Transports;

[Serializable]
public abstract class Transport
{
    private double _averageSpeed;
    private string _name;

    protected Transport(string name, double averageSpeed)
    {
        Name = name;
        AverageSpeed = averageSpeed;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Имя не может быть пустым");
            _name = value;
        }
    }

    public double AverageSpeed
    {
        get => _averageSpeed;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Средняя скорость должна быть положительной");
            _averageSpeed = value;
        }
    }
}