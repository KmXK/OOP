using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Figures;
using Figures.Creators.Point;
using Figures.Drawers.WPF;
using Ellipse = Figures.Ellipse;
using Point = Figures.Point;
using Polyline = Figures.Polyline;
using Rectangle = Figures.Rectangle;

namespace FiguresDrawing;

public partial class MainWindow : Window, IPointInputable
{
    private readonly FiguresList _figures = new()
    {
        new Rectangle(new(100, 100), new(700, 400)),
        new Polyline(new(100, 100), new(700, 400)),
        new Ellipse(100, 50, new(400, 200)),
        new Circle(200, new(0, 0))
    };

    private readonly IList<(string Name, IFigureCreator Creator, PreviewFactory PreviewFactory)> _creators =
        new List<(string, IFigureCreator, PreviewFactory)>
        {
            ("Circle", new CircleCreator(), new RectanglePreviewFactory()),
            ("Rectangle", new RectangleCreator(),new RectanglePreviewFactory()),
            ("Square", new SquareCreator(), new RectanglePreviewFactory()),
            ("Ellipse", new EllipseCreator(), new RectanglePreviewFactory()),
            ("Polyline", new PolylineCreator(), new PolylinePreviewFactory()),
            ("Polygon", new PolygonCreator(), new PolylinePreviewFactory()),
            ("Triangle", new TriangleCreator(), new PolylinePreviewFactory(3))
        };

    private readonly FiguresWpfDrawer[] _drawers;
    private readonly Dictionary<Figure, Shape> _shapes = new();

    private bool _isMouseDown = false;

    private (string Name, IFigureCreator Creator, PreviewFactory PreviewFactory) CurrentCreatorData =>
        _creators[ComboBox.SelectedIndex];

    private Preview _preview;

    public MainWindow()
    {
        InitializeComponent();
        InitializeCombobox();

        // Initializing figure drawers array
        _drawers = new FiguresWpfDrawer[]
        {
            new PolylineDrawer(DrawCanvas, Brushes.Black, Brushes.Transparent),
            new EllipseDrawer(DrawCanvas, Brushes.Black, Brushes.Transparent)
        };

        // Drawing start figures
        foreach(var figure in _figures)
        {
            DrawFigure(figure);
        }
    }

    private void InitializeCombobox()
    {
        foreach (var creator in _creators)
        {
            var item = new ComboBoxItem()
            {
                Content = creator.Name
            };
            ComboBox.Items.Add(item);
        }

        ComboBox.SelectedIndex = 0;
    }

    private void AddFigure(Figure figure)
    {
        if (figure == null)
            throw new ArgumentNullException(nameof(figure));

        _figures.Add(figure);
    }

    private void DrawFigure(Figure figure)
    {
        if (figure == null)
            return;

        foreach (var drawer in _drawers)
        {
            var shape = drawer.Draw(figure);
            if (shape != null) _shapes[figure] = shape;
        }
    }

    private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_isMouseDown)
            return;
            
        _isMouseDown = true;
        _preview?.MouseDown(e.GetPosition(DrawCanvas));
    }

    private void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (!_isMouseDown)
            return;
            
        _isMouseDown = false;
        _preview.MouseUp(e.GetPosition(DrawCanvas));
    }

    private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!_isMouseDown)
            return;

        _preview.MouseMove(e.GetPosition(DrawCanvas));
    }

    private void DrawButton_Click(object sender, RoutedEventArgs e)
    {
        _preview.RequestToDraw();
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _preview = CurrentCreatorData.PreviewFactory.CreatePreview(DrawCanvas);
        _preview.Drawn += (_, _) => DrawCurrentFigure();
    }

    private void StopDrawing()
    {
        _isMouseDown = false;
    }

    private void DrawCurrentFigure()
    {
        var figure = CurrentCreatorData.Creator.Create(this);
        if (figure == null)
            return;
        AddFigure(figure);
        DrawFigure(figure);
        StopDrawing();
    }

    private void Window_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && _preview.HasPreview && !e.IsRepeat)
        {
            e.Handled = true;
            StopDrawing();
        }
        else if (!_preview.HasPreview && e.Key == Key.Z &&
                 e.KeyboardDevice.Modifiers == ModifierKeys.Control)
        {
            if (!_figures.Any())
                return;
            var figure = _figures[^1];
            DrawCanvas.Children.Remove(_shapes[figure]);
            _shapes.Remove(figure);
            _figures.Remove(figure);
        }
    }

    public bool InputPoints(out Point[] points)
    {
        points = _preview.Points;
        return points.Any();
    }

    public bool InputRectArea(out Point leftTop, out Point rightBottom)
    {
        var points = _preview.Points;
        leftTop = rightBottom = default;

        if (points == null || points.Length < 2)
            return false;

        leftTop = points[0];
        rightBottom = points[1];
        return true;
    }
}