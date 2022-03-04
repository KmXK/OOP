using System.Windows.Controls;

namespace FiguresDrawing;

public class RectanglePreviewFactory : PreviewFactory
{
    public override Preview CreatePreview(Canvas canvas)
    {
        return new RectanglePreview(canvas);
    }
}