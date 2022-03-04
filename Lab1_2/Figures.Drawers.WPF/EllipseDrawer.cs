using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Drawers.WPF;

public class EllipseDrawer : FiguresWpfDrawer
{
    public EllipseDrawer(Canvas canvas, Brush stroke, Brush fill)
    {
        Fill = fill ?? Brushes.White;
        Stroke = stroke ?? Brushes.Black;
        Canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    }

    public override Shape Draw(Figure figure)
    {
        if (Canvas == null || figure is not Ellipse el)
            return null;

        var e = new System.Windows.Shapes.Ellipse
        {
            Height = el.B * 2, Width = el.A * 2,
            Stroke = Stroke,
            Fill = Fill
        };

        e.SetValue(Canvas.LeftProperty, (double)el.Center.X - el.A);
        e.SetValue(Canvas.TopProperty, (double)el.Center.Y - el.B);
        Canvas.Children.Add(e);
        return e;
    }
}