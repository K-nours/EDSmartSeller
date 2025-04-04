namespace EDSS_Core;

using System.Runtime.InteropServices;

public struct MAC_POINT
{
    public double X;
    public double Y;
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

}