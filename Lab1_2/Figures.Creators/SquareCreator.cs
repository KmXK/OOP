namespace Figures.Creators.Point;

public class SquareCreator : IFigureCreator
{
    public Figure Create(IPointInputable inputPointService)
    {
        if (!inputPointService.InputRectArea(out var first, out var second))
            return null;

        var dx = first.X - second.X;
        var dy = first.Y - second.Y;
            
        var width = Math.Min(Math.Abs(dx), Math.Abs(dy));
        dx = Math.Sign(dx) * width;
        dy = Math.Sign(dy) * width;

        second = new Figures.Point(first.X - dx, first.Y - dy);

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