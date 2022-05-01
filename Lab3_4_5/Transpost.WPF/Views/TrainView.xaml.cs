using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Transports.WPF.Views;

public partial class TrainView : UserControl, ITransportView
{
    public TrainView()
    {
        InitializeComponent();
    }

    public void Set(Transport transport)
    {
        if (transport is not Train train)
            throw new ArgumentException();

        Model.Text = train.Name;
        AvgSpeed.Text = train.AverageSpeed.ToString(CultureInfo.InvariantCulture);
        WagonsCount.Text = train.WagonsCount.ToString();
    }

    public event Action<Transport> Confirmed;

    private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Confirmed?.Invoke(new Train(Model.Text,
                Convert.ToDouble(AvgSpeed.Text, CultureInfo.InvariantCulture),
                int.Parse(WagonsCount.Text)));
        }
        catch
        {
            MessageBox.Show("Некорректные данные!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}