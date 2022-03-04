namespace Figures.Creators.Point;

public interface IPointInputable
{
    bool InputPoints(out Figures.Point[] points);

    bool InputRectArea(out Figures.Point leftTop, out Figures.Point rightBottom);
}