using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FiguresDrawing;

public abstract class Preview
{
    protected readonly Canvas _canvas;

    protected Preview(Canvas canvas)
    {
        _canvas = canvas;
    }

    protected Shape _previewShape;

    public abstract Figures.Point[] Points { get; }

    public event EventHandler Drawn;

    public bool HasPreview => _previewShape != null;

    public abstract void MouseDown(Point p);
    public abstract void MouseMove(Point p);
    public abstract void MouseUp(Point p);

    public virtual void RequestToDraw() { }

    public virtual void Reset()
    {
        if(_previewShape != null)
            _canvas.Children.Remove(_previewShape);
        _previewShape = null;
    }

    public virtual void OnDrawn()
    {
        Drawn?.Invoke(this, new PreviewEventArgs(Points));
        Reset();
    }
}