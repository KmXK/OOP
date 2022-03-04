using System.Windows.Controls;

namespace FiguresDrawing;

public class PolylinePreviewFactory : PreviewFactory
{
    public override Preview CreatePreview(Canvas canvas)
    {
        return new PolylinePreview(canvas);
    }
}