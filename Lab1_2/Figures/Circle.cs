namespace Figures;

public sealed class Circle : Ellipse
{
    public int Radius => A;

    public Circle(int radius, Point center):
        base(radius, radius, center)
    {
            
    }
}