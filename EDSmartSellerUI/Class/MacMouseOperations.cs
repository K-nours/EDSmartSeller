namespace EDSmartSellerUI;
using System.Runtime.InteropServices;

internal class MacMouseOperations : IMouseOperations
{
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern IntPtr CGEventCreateMouseEvent(IntPtr source, int mouseType, POINT mousePosition, int mouseButton);

    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
    private static extern void CGEventPost(int tap, IntPtr @event);


    private const int kCGEventLeftMouseDown = 1;
    private const int kCGEventLeftMouseUp = 2;
    private const int kCGHIDEventTap = 0;

    public POINT GetCursorPositon()
    {
        throw new NotImplementedException();
    }

    public void MoveCursor(POINT location)
    {
        throw new NotImplementedException();
    }

    public void LeftClick(POINT position)
    {
       
        var down = CGEventCreateMouseEvent(IntPtr.Zero, kCGEventLeftMouseDown, position, 0);
        var up = CGEventCreateMouseEvent(IntPtr.Zero, kCGEventLeftMouseUp, position, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(50);
        CGEventPost(kCGHIDEventTap, up);
    }


    public void LeftClick(POINT position, int stayPushMs)
    {
        var down = CGEventCreateMouseEvent(IntPtr.Zero, kCGEventLeftMouseDown, position, 0);
        var up = CGEventCreateMouseEvent(IntPtr.Zero, kCGEventLeftMouseUp, position, 0);

        CGEventPost(kCGHIDEventTap, down);
        Thread.Sleep(stayPushMs);
        CGEventPost(kCGHIDEventTap, up);
    }


}
