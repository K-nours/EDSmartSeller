namespace EDSS_Core;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MAC_POINT
{
    public double X;
    public double Y;

    public MAC_POINT(double x, double y)
    {
        X = x;
        Y = y;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct WIN_POINT
{
    public int X;
    public int Y;
}

public class POINT
{
    public MAC_POINT MacPoint;
    public WIN_POINT WinPoint;

    public int win_x { get => WinPoint.X; }
    public int win_y { get => WinPoint.Y; }

    public double mac_x { get => MacPoint.X; }
    public double mac_y { get => MacPoint.Y; }

    public POINT(WIN_POINT point)
    { WinPoint = point; }

    public POINT(MAC_POINT point)
    { MacPoint = point; }

    public POINT() { }

    public string displayX
    {
        get
        {
            if (win_x >= 0 && mac_x >= 0)
            {
                double maxValue = (win_x > mac_x) ? win_x : mac_x;
                return maxValue.ToString();
            }
            else
            {
                double minValue = (win_x < mac_x) ? win_x : mac_x;
                return minValue.ToString();
            }
        }
    }

    public string displayY
    {
        get
        {
            if (win_y >= 0 && mac_y >= 0)
            {
                double maxValue = (win_y > mac_y) ? win_y : mac_y;
                return maxValue.ToString();
            }
            else
            {
                double minValue = (win_y < mac_y) ? win_y : mac_y;
                return minValue.ToString();
            }
        }
    }
}