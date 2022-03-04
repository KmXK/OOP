namespace Figures;

public class Ellipse : Figure
{
    public int A { get; init; }
    public int B { get; init; }
    public Point Center { get; init; }

    public Ellipse(int a, int b, Point center)
    {
        A = a;
        B = b;
        Center = center;
    }
}