namespace Figures.Creators.Point;

public class PolygonCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputPoints(out var points))
        {
            return null;
        }

        try
        {
            return new Polygon(points);
        }
        catch
        {
            return null;
        }
    }
}