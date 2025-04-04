namespace EDSmartSellerUI.Class;
using System.Runtime.InteropServices;
using EDSS_Core;

internal class MacMouseOperations : IMouseOperations
{
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern nint CGEventCreateMouseEvent(nint source, int mouseType, MAC_POINT mousePosition, int mouseButton);

    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern void CGEventPost(int tap, nint @event);


    private const int kCGEventLeftMouseDown = 1;
    private const int kCGEventLeftMouseUp = 2;
    private const int kCGHIDEventTap = 0;

    public MAC_POINT GetCursorPositon()
    {
        throw new NotImplementedException();
    }

    public void MoveCursor(MAC_POINT location)
    {
        throw new NotImplementedException();
    }

    public void LeftClick(MAC_POINT position)
    {
       
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, position, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, position, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(50);
        CGEventPost(kCGHIDEventTap, up);
    }


    public void LeftClick(POINT position, int stayPushMs)
    {
        var down = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseDown, position, 0);
        var up = CGEventCreateMouseEvent(nint.Zero, kCGEventLeftMouseUp, position, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(stayPushMs);
        CGEventPost(kCGHIDEventTap, up);
    }


}
