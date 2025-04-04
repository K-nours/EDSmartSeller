namespace EDSS_Core.MousseOperations;
using System.Runtime.InteropServices;
using EDSS_Core;

public class MacMouseOperations : IMouseOperations
{
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern nint CGEventCreateMouseEvent(nint source, int mouseType, MAC_POINT mousePosition, int mouseButton);
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern void CGEventPost(int tap, nint @event);
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern MAC_POINT CGEventGetLocation(nint @event);

    private const int kCGEventLeftMouseDown = 1;
    private const int kCGEventLeftMouseUp = 2;
    private const int kCGHIDEventTap = 0;
    private const int kCGEventMouseMoved = 5;

    public POINT GetCursorPositon()
    {
        var eventRef = CGEventCreateMouseEvent(0, kCGEventMouseMoved, new MAC_POINT(), 0);
        var location = CGEventGetLocation(eventRef);
        return new POINT(location);
    }

    public void MoveCursor(POINT location)
    {
        var mac_point = new MAC_POINT { X = location.mac_x, Y = location.mac_y };
        var mouseMoveEvent = CGEventCreateMouseEvent(0, kCGEventMouseMoved, mac_point, 0);
        CGEventPost(kCGHIDEventTap, mouseMoveEvent);
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
}