namespace Transports;

[Serializable]
public class Airplane : Transport
{
    private int _maxFlightAltitude;

    public Airplane(string name, double averageSpeed, int maxFlightAltitude) : base(name, averageSpeed)
    {
        MaxFlightAltitude = maxFlightAltitude;
    }
        
    public int MaxFlightAltitude
    {
        get => _maxFlightAltitude;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Максимальная высота полёта должна быть положительной");
            _maxFlightAltitude = value;
        }
    }
}