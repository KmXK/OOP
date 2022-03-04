using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Drawers.WPF;

public class PolylineDrawer : FiguresWpfDrawer
{
    public PolylineDrawer(Canvas canvas, Brush stroke, Brush fill)
    {
        Fill = fill ?? Brushes.White;
        Stroke = stroke ?? Brushes.Black;
        Canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    }

    public override Shape Draw(Figure figure)
    {
        if (Canvas == null || figure is not Polyline line)
            return null;

        var p = new System.Windows.Shapes.Polyline
        {
            Points = new PointCollection(line.Points.Select(p => p.ConvertPointToWpf())),
            Stroke = Stroke,
            Fill = Fill
        };
        Canvas.Children.Add(p);
        return p;
    }
}