using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Figures.Drawers.WPF;

namespace FiguresDrawing;

public class PolylinePreview : Preview
{
    public PolylinePreview(Canvas canvas) : base(canvas)
    {
    }

    public override Figures.Point[] Points
    {
        get
        {
            return (_previewShape as Polyline)?.Points
                .Select(p => p.ConvertPointFromWpf()).ToArray();
        }
    }

    public override void MouseDown(Point p)
    {
        if (_previewShape == null)
        {
            _previewShape = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection(new double[]{4, 4})
            };
            var polyline = (Polyline) _previewShape;
            polyline.Points.Add(p);
            _canvas.Children.Add(_previewShape);
        }
        ((Polyline)_previewShape).Points.Add(p);
    }

    public override void MouseMove(Point p)
    {
        var polyline = (Polyline)_previewShape;
        polyline.Points[^1] = p;
    }

    public override void MouseUp(Point p)
    {
        var polyline = (Polyline)_previewShape;
        polyline.Points[^1] = p;
    }
}