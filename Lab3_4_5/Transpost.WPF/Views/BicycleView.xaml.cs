using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Transports.WPF.Views;

public partial class BicycleView : UserControl, ITransportView
{
    public BicycleView()
    {
        InitializeComponent();
    }

    public void Set(Transport transport)
    {
        if (transport is not Bicycle bicycle)
            throw new ArgumentException();

        Model.Text = bicycle.Name;
        AvgSpeed.Text = bicycle.AverageSpeed.ToString(CultureInfo.InvariantCulture);
        IsElectric.IsChecked = bicycle.IsElectric;
    }

    public event Action<Transport> Confirmed;

    private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Confirmed?.Invoke(new Bicycle(Model.Text,
                Convert.ToDouble(AvgSpeed.Text, CultureInfo.InvariantCulture),
                IsElectric.IsChecked == true));
        }
        catch
        {
            MessageBox.Show("Некорректные данные!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}