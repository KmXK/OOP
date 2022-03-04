using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Figures.Drawers.WPF;

namespace FiguresDrawing;

public class RectanglePreview : Preview
{
    private double x, y;

    public RectanglePreview(Canvas canvas) : base(canvas)
    {
    }

    public override Figures.Point[] Points
    {
        get
        {
            if (_previewShape == null)
                return null;
            var left = Canvas.GetLeft(_previewShape);
            var top = Canvas.GetTop(_previewShape);
            var right = left + _previewShape.Width;
            var bottom = top + _previewShape.Height;
            return new[]
            {
                new Point(left, top).ConvertPointFromWpf(),
                new Point(right, bottom).ConvertPointFromWpf()
            };
        }
    }

    public override void MouseDown(Point p)
    {
        _previewShape = new Rectangle
        {
            Stroke = Brushes.Black,
            StrokeDashArray = new DoubleCollection(new double[]{4, 4})
        };
        Canvas.SetLeft(_previewShape, p.X);
        Canvas.SetTop(_previewShape, p.Y);
        _previewShape.Width = _previewShape.Height = 0;
        _canvas.Children.Add(_previewShape);

        x = p.X;
        y = p.Y;
    }

    public override void MouseMove(Point p)
    {
        var width = p.X - x;
        var height = p.Y - y;

        if (width < 0)
        {
            width = -width;
            Canvas.SetLeft(_previewShape, p.X);
        }

        if (height < 0)
        {
            height = -height;
            Canvas.SetTop(_previewShape, p.Y);
        }

        _previewShape.Width = width;
        _previewShape.Height = height;
    }

    public override void MouseUp(Point p)
    {
        OnDrawn();
        Reset();
    }
}