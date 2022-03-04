namespace Figures.Creators.Point;

public interface IFigureCreator
{
    Figure Create(IPointInputable inputPointService);
}