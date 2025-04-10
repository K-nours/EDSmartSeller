namespace EDSS_Core.MousseOperations;

using EDSS_Core;
using System.Runtime.InteropServices;

public class MacMouseOperations : IMouseOperations
{
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern nint CGEventCreateMouseEvent(nint source, int mouseType, MAC_POINT mousePosition, int mouseButton);
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern void CGEventPost(int tap, nint @event);
    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern MAC_POINT CGEventGetLocation(IntPtr eventRef);
    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern IntPtr CGEventCreate(IntPtr source);
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern void CGWarpMouseCursorPosition(MAC_POINT newPosition);

    private const int kCGEventLeftMouseDown = 1;
    private const int kCGEventLeftMouseUp = 2;
    private const int kCGHIDEventTap = 0;
    private const int kCGEventMouseMoved = 5;

    public POINT GetCursorPositon()
    {
        IntPtr eventRef = CGEventCreate(IntPtr.Zero);
        MAC_POINT location = CGEventGetLocation(eventRef);
        WriteDebug(location.X + " " + location.Y);
        return new POINT(location);
    }

    public void MoveCursor(POINT location)
    {
        var mac_point = new MAC_POINT { X = location.mac_x, Y = location.mac_y };
        var move = CGEventCreateMouseEvent(nint.Zero ,kCGEventMouseMoved, mac_point,0);
        
        CGWarpMouseCursorPosition(mac_point);
        CGEventPost(kCGHIDEventTap, move);
    }

    public void LeftClick(POINT position)
    {
        var macPoint = new MAC_POINT { X = position.mac_x, Y = position.mac_y };
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, macPoint, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, macPoint, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(50);
        CGEventPost(kCGHIDEventTap, up);
    }


    public void LeftClick(POINT position, int stayPushMs)
    {
        var macPoint = new MAC_POINT { X = position.mac_x, Y = position.mac_y };
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, macPoint, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, macPoint, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(stayPushMs);
        CGEventPost(kCGHIDEventTap, up);
    }

    private void WriteDebug(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(msg);
        Console.ResetColor();
    }
}