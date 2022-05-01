namespace Transports;

[Serializable]
public class Bicycle : Transport
{
    public Bicycle(string name, double averageSpeed, bool isElectric) : base(name, averageSpeed)
    {
        IsElectric = isElectric;
    }

    public bool IsElectric { get; set; }
}