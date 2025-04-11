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
        return new POINT(location);
    }

    public void MoveCursor(POINT location)
    {
        var move = CGEventCreateMouseEvent(nint.Zero, kCGEventMouseMoved, location.MacPoint, 0);

        CGWarpMouseCursorPosition(location.MacPoint);
        CGEventPost(kCGHIDEventTap, move);
    }

    public void LeftClick(POINT position)
    {
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, position.MacPoint, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, position.MacPoint, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(50);
        CGEventPost(kCGHIDEventTap, up);
    }


    public void LeftClick(POINT position, int stayPushMs)
    {
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, position.MacPoint, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, position.MacPoint, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(stayPushMs);
        CGEventPost(kCGHIDEventTap, up);
    }
}