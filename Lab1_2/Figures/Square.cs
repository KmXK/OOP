namespace Figures;

public sealed class Square : Rectangle
{
    public Square(Point a, Point b) : base(a, b)
    {
        if (Math.Abs(a.X - b.X) != Math.Abs(a.Y - b.Y))
            throw new ArgumentException("Width must equals height.");
    }

    public Square(Point topLeft, int width, int height) :
        base(topLeft,
            topLeft with {X = topLeft.X + width, Y = topLeft.Y + height})
    {

    }
}