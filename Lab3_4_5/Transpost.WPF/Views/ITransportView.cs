using System;

namespace Transports.WPF.Views;

public interface ITransportView
{
    void Set(Transport transport);

    event Action<Transport> Confirmed;
}