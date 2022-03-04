using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Drawers.WPF;

public abstract class FiguresWpfDrawer
{
    public Canvas Canvas { get; set; }
    public Brush Stroke { get; set; }
    public Brush Fill { get; set; }
    public abstract Shape Draw(Figure figure);
}