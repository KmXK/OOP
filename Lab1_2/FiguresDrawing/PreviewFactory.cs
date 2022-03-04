using System.Windows.Controls;

namespace FiguresDrawing;

public abstract class PreviewFactory
{
    public abstract Preview CreatePreview(Canvas canvas);
}