namespace EDSmarteSeller
{
    using EDSS_Core;
    using EDSS_Core.MousseOperations;
    using System;
    using System.Runtime.InteropServices;

    internal class TestMouseBehaviour(IMouseOperations mouseManager)
    {
        //[StructLayout(LayoutKind.Sequential)]
        //public struct CGPoint
        //{
        //    public double X;
        //    public double Y;
        //}

        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern MAC_POINT CGEventGetLocation(IntPtr eventRef);

        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGEventCreate(IntPtr source);

        // Importing CoreGraphics function to move the mouse
        [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
        private static extern void CGWarpMouseCursorPosition(MAC_POINT newPosition);

        public void MoveMouseTo()
        {                  
            Console.Write("Enter mousse new coord (\"x:y\") : ");
            var coordStr = Console.ReadLine()!;

            var x = float.Parse(coordStr.Split(':')[0]);
            var y = float.Parse(coordStr.Split(":")[1]);

            //var point = new POINT(new MAC_POINT(x, y));
            //mouseManager.MoveCursor(point);
            var point = new MAC_POINT { X = x, Y = y };


            CGWarpMouseCursorPosition(point);
            Console.WriteLine($"Moving cursor to {x}:{y}");
            //var newLocation = mouseManager.GetCursorPositon();
            //Console.WriteLine($"New position read {newLocation.displayX}:{newLocation.displayY}");
            GetMacMouseLocation();

            var point2 = new POINT(point);
            mouseManager.LeftClick(point2);

        }
        public void GetMacMouseLocation()
        {
            IntPtr eventRef = CGEventCreate(IntPtr.Zero);
            MAC_POINT point = CGEventGetLocation(eventRef);
            Console.WriteLine($"Pointer Location: X = {point.X}, Y = {point.Y}");
        }
    }
}
