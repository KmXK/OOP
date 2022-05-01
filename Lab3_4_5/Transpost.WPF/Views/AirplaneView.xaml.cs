using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Transports.WPF.Views;

public partial class AirplaneView : UserControl, ITransportView
{
    public AirplaneView()
    {
        InitializeComponent();
    }

    public void Set(Transport transport)
    {
        if (transport is not Airplane airplane)
            throw new ArgumentException();

        Model.Text = airplane.Name;
        AvgSpeed.Text = airplane.AverageSpeed.ToString(CultureInfo.InvariantCulture);
        MaxFlightAltitude.Text = airplane.MaxFlightAltitude.ToString();
    }

    public event Action<Transport> Confirmed;

    private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Confirmed?.Invoke(new Airplane(Model.Text,
                Convert.ToDouble(AvgSpeed.Text, CultureInfo.InvariantCulture),
                int.Parse(MaxFlightAltitude.Text)));
        }
        catch
        {
            MessageBox.Show("Некорректные данные!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}