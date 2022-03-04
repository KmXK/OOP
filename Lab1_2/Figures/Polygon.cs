namespace Figures;

public class Polygon : Polyline
{
    public Polygon(params Point[] points) : base(points)
    {
        if (!_points[0].Equals(_points[^1]))
        {
            Array.Resize(ref _points, _points.Length + 1);
            _points[^1] = _points[0];
        }
    }
}