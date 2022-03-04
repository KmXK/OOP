using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Drawers.WPF;

public abstract class FiguresWpfDrawer
{
    public Canvas Canvas { get; init; }
    public Brush Stroke { get; init; }
    public Brush Fill { get; init; }
    public abstract Shape Draw(Figure figure);
}