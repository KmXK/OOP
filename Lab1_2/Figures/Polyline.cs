namespace Figures;

public class Polyline : Figure
{
    protected Point[] _points;

    public Polyline(params Point[] points)
    {
        if (points == null) throw new ArgumentNullException(nameof(points));
        if (points.Length < 2) throw new ArgumentException("Points count cannot be less than 2");

        _points = (Point[])points.Clone();
    }

    public Point[] Points => (Point[])_points.Clone();
}