using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Transports.WPF.Views;

namespace Transports.WPF;

public partial class MainWindow : Window
{
    private readonly IDictionary<Type, (string Name, Func<ITransportView> CreateFunc)> _viewCreators =
        new Dictionary<Type, (string, Func<ITransportView>)>
        {
            {typeof(Car), ("Car", () => new CarView())},
            {typeof(Bicycle), ("Bicycle", () => new BicycleView())},
            {typeof(Airplane), ("Airplane", () => new AirplaneView())},
            {typeof(Submarine), ("Submarine", () => new SubmarineView())},
            {typeof(Train), ("Train", () => new TrainView())}
        };

    private readonly PluginSerializer<ObservableCollection<Transport>> _serializer;

    public ObservableCollection<Transport> Transports { get; private set; } = new();

    public MainWindow()
    {
        InitializeComponent();
        InitializeComboBox();
        InitializePlugins();

        DataContext = this;

        _serializer = new PluginSerializer<ObservableCollection<Transport>>(new BinarySerializer<ObservableCollection<Transport>>());
    }

    private void InitializeComboBox()
    {
        TransportComboBox.Items.Clear();
        foreach (var comboBoxItem in _viewCreators.Values.Select(c => new ComboBoxItem {Content = c.Name}))
        {
            TransportComboBox.Items.Add(comboBoxItem);
        }

        TransportComboBox.SelectedIndex = 0;
    }

    private void InitializePlugins()
    {
        if (PluginLoader.Instance.Plugins.Any())
        {
            PluginGrid.Visibility = Visibility.Visible;
            
            PluginComboBox.Items.Clear();
            PluginComboBox.Items.Add(new ComboBoxItem {Content = "Без плагинов"});
            PluginComboBox.SelectedIndex = 0;
            PluginComboBox.SelectionChanged += PluginComboBox_Changed;

            foreach (var plugin in PluginLoader.Instance.Plugins)
            {
                PluginComboBox.Items.Add(new ComboBoxItem {Content = plugin.Name});
            }
        }
        else
        {
            PluginGrid.Visibility = Visibility.Collapsed;
        }
    }

    private void DeleteBtn_Click(object sender, RoutedEventArgs e)
    {
        if (ListBox.SelectedIndex >= 0)
        {
            var index = ListBox.SelectedIndex;

            Transports.RemoveAt(index);

            if (Transports.Count > index)
                ListBox.SelectedIndex = index;
            else if (Transports.Count > 0)
                ListBox.SelectedIndex = index - 1;
        }
    }

    private void CreateBtn_Click(object sender, RoutedEventArgs e)
    {
        var view = _viewCreators.Skip(TransportComboBox.SelectedIndex).First().Value.CreateFunc();
        ListBox.SelectedIndex = -1;
        SelectView(view);
        view.Confirmed += transport =>
        {
            Transports.Add(transport);
            ListBox.SelectedIndex = Transports.Count - 1;
            ResetView();
        };
    }

    private void ResetView()
    {
        ViewContainer.Child = null;
    }

    private void SelectView(ITransportView view)
    {
        ViewContainer.Child = (UserControl)view;
    }

    private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ResetView();

        if (ListBox.SelectedIndex >= 0)
        {
            var t = Transports[ListBox.SelectedIndex];
            if(_viewCreators.TryGetValue(t.GetType(), out var creator))
            {
                var view = creator.CreateFunc();
                SelectView(view);
                view.Set(t);

                view.Confirmed += transport =>
                {
                    var index = ListBox.SelectedIndex;
                    Transports[index] = transport;

                    MessageBox.Show("Запись была изменена! ", "Изменение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                };
            }
        }
    }

    private void SerializeBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            using var fs = File.Open("Data.bin", FileMode.Create);
            _serializer.Serialize(fs, Transports);

            MessageBox.Show("Сериализация была успешна завершена.\nДанные были сохранены в файл Data.bin.", "Сериализация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch
        {
            MessageBox.Show("Ошибка во время сериализации!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void DeserializeBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            using var fs = File.OpenRead("Data.bin");
            ListBox.ItemsSource = Transports = _serializer.Deserialize(fs);

            MessageBox.Show("Десериализация была успешна завершена.", "Десериализация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (IOException)
        {
            MessageBox.Show("Ошибка при чтении файла Data.bin!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Ошибка при десериализации!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void PluginComboBox_Changed(object sender, SelectionChangedEventArgs e)
    {
        var index = PluginComboBox.SelectedIndex;
        _serializer.SetPlugin(index >= 1 ? PluginLoader.Instance.Plugins[index - 1] : null);
    }
}