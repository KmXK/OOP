namespace Figures;

public class Rectangle : Polygon
{
    public int Width => Math.Abs(_points[0].X - _points[1].X);
    public int Height => Math.Abs(_points[1].Y - _points[2].Y);

    public Rectangle(Point a, Point b) :
        base(a, a with {X = b.X}, b, b with {X = a.X})

    {

    }
}