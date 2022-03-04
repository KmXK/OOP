namespace Figures.Drawers.WPF;

public static class PointHelper
{
    public static System.Windows.Point ConvertPointToWpf(this Point point)
    {
        return new(point.X, point.Y);
    }

    public static Point ConvertPointFromWpf(this System.Windows.Point point)
    {
        return new((int)point.X, (int)point.Y);
    }
}