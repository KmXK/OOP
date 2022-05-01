namespace Transports;

[Serializable]
public class Submarine : Transport
{
    private int _maxImmersionDepth;

    public Submarine(string name, double averageSpeed, int maxImmersionDepth) : base(name, averageSpeed)
    {
        MaxImmersionDepth = maxImmersionDepth;
    }

    public int MaxImmersionDepth
    {
        get => _maxImmersionDepth;
        set
        {
            if(value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Максимальная высота погружения должна быть положительной");
            _maxImmersionDepth = value;
        }
    }
}