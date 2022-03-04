namespace Figures.Creators.Point;

public class PolylineCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputPoints(out var points))
        {
            return null;
        }

        try
        {
            return new Polyline(points);
        }
        catch
        {
            return null;
        }
    }
}