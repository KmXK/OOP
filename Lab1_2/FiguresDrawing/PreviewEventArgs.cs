using System;
using Figures;

namespace FiguresDrawing;

public class PreviewEventArgs : EventArgs
{
    public Point[] Points { get; init; }

    public PreviewEventArgs(Point[] points)
    {
        Points = points;
    }
}