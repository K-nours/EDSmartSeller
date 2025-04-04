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