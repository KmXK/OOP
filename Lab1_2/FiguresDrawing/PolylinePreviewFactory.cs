using System.Windows.Controls;

namespace FiguresDrawing;

public class PolylinePreviewFactory : PreviewFactory
{
    private int _validCount = -1;

    public PolylinePreviewFactory()
    {
        
    }

    public PolylinePreviewFactory(int validCount)
    {
        _validCount = validCount;
    }

    public override Preview CreatePreview(Canvas canvas)
    {
        return _validCount == -1 ? new PolylinePreview(canvas) : new PolylinePreview(canvas, _validCount);
    }
}