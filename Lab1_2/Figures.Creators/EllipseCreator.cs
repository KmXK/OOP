namespace Figures.Creators.Point;

public class EllipseCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputRectArea(out var first, out var second))
            return null;

        var center = new Figures.Point((first.X + second.X) / 2, (first.Y + second.Y) / 2);
        var a = Math.Abs(center.X - first.X);
        var b = Math.Abs(center.Y - first.Y);

        try
        {
            return new Ellipse(a, b, center);
        }
        catch
        {
            return null;
        }
    }
}