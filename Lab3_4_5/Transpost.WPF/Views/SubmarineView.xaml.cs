using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Transports.WPF.Views;

public partial class SubmarineView : UserControl, ITransportView
{
    public SubmarineView()
    {
        InitializeComponent();
    }

    public void Set(Transport transport)
    {
        if (transport is not Submarine submarine)
            throw new ArgumentException();

        Model.Text = submarine.Name;
        AvgSpeed.Text = submarine.AverageSpeed.ToString(CultureInfo.InvariantCulture);
        MaxImmersionDepth.Text = submarine.MaxImmersionDepth.ToString();
    }

    public event Action<Transport> Confirmed;

    private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Confirmed?.Invoke(new Submarine(Model.Text,
                Convert.ToDouble(AvgSpeed.Text, CultureInfo.InvariantCulture),
                int.Parse(MaxImmersionDepth.Text)));
        }
        catch
        {
            MessageBox.Show("Некорректные данные!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}