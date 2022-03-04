namespace Figures.Creators.Point;

public class CircleCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputRectArea(out var first, out var second))
            return null;

        var center = new Figures.Point((first.X + second.X) / 2, (first.Y + second.Y) / 2);
        var radius = Math.Min(Math.Abs(center.X - first.X), Math.Abs(center.Y - first.Y));

        try
        {
            return new Circle(radius, center);
        }
        catch
        {
            return null;
        }
    }
}