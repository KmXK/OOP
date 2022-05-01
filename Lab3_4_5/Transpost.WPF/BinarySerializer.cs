using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Transports.WPF;

public class BinarySerializer<T> : ISerializer<T>
{
    public void Serialize(Stream stream, T data)
    {
        var bf = new BinaryFormatter();
        bf.Serialize(stream, data);
    }

    public T Deserialize(Stream stream)
    {
        var bf = new BinaryFormatter();
        return (T) bf.Deserialize(stream);
    }
}