namespace Figures.Creators.Point;

public class TriangleCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputPoints(out var points))
        {
            return null;
        }

        try
        {
            return new Triangle(points);
        }
        catch
        {
            return null;
        }
    }
}