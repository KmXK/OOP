namespace Figures.Creators.Point;

public class RectangleCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputRectArea(out var first, out var second))
            return null;

        try
        {
            return new Rectangle(first, second);
        }
        catch
        {
            return null;
        }
    }
}