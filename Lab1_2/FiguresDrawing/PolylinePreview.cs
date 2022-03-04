using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Figures.Drawers.WPF;

namespace FiguresDrawing;

public class PolylinePreview : Preview
{
    private int _validCount;
    private int _count;
    private bool _hasMoved;

    public PolylinePreview(Canvas canvas) : base(canvas)
    {
        _validCount = -1;
    }

    public PolylinePreview(Canvas canvas, int minCount) : base(canvas)
    {
        _validCount = Math.Max(2, minCount);
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
                StrokeDashArray = new DoubleCollection(new double[] {4, 4})
            };
            _canvas.Children.Add(_previewShape);
            _count = 0;
        }
        _hasMoved = false;
        ((Polyline)_previewShape).Points.Add(p);
        _count++;
    }

    public override void MouseMove(Point p)
    {
        var polyline = (Polyline)_previewShape;
        if (!_hasMoved && _count == 1)
        {
            polyline.Points.Add(p);
            _count++;
        }

        polyline.Points[^1] = p;
        
        _hasMoved = true;
    }

    public override void MouseUp(Point p)
    {
        if (!_hasMoved && _count == 1)
        {
            return;
        }

        var polyline = (Polyline)_previewShape;
        polyline.Points[^1] = p;

        if (_count == _validCount)
        {
            OnDrawn();
        }
    }

    public override void RequestToDraw()
    {
        if (HasPreview && _validCount == -1)
        {
            OnDrawn();
        }
        else
        {
            Reset();
        }
    }

    public override void Reset()
    {
        base.Reset();
        _count = 0;
    }
}