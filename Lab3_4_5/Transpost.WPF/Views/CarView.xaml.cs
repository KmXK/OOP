using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Transports.WPF.Views;

public partial class CarView : UserControl, ITransportView
{
    public CarView()
    {
        InitializeComponent();
    }

    public void Set(Transport transport)
    {
        if (transport is not Car car)
            throw new ArgumentException();

        Model.Text = car.Name;
        AvgSpeed.Text = car.AverageSpeed.ToString(CultureInfo.InvariantCulture);
        WheelsCount.Text = car.WheelsCount.ToString();
    }

    public event Action<Transport> Confirmed;

    private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Confirmed?.Invoke(new Car(Model.Text,
                Convert.ToDouble(AvgSpeed.Text, CultureInfo.InvariantCulture),
                int.Parse(WheelsCount.Text)));
        }
        catch
        {
            MessageBox.Show("Некорректные данные!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}