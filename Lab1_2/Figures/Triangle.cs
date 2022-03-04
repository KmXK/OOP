namespace Figures;

public class Triangle : Polygon
{
   public Triangle(params Point[] points) : base(points)
   {
       if (points == null || points.Length != 3)
           throw new ArgumentException(nameof(points));
   }
}