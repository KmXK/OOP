using System.IO;

namespace Transports.WPF;

public interface ISerializer<T>
{
    void Serialize(Stream stream, T data);
    T Deserialize(Stream stream);
}