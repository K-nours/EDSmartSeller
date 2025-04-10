namespace EDSmarteSeller
{
    using EDSS_Core;
    using EDSS_Core.MousseOperations;
    using System;
    using System.Runtime.InteropServices;

    internal class TestMouseBehaviour(IMouseOperations mouseManager)
    {
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern MAC_POINT CGEventGetLocation(IntPtr eventRef);
        [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
        private static extern IntPtr CGEventCreate(IntPtr source);
        [DllImport("/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices")]
        private static extern void CGWarpMouseCursorPosition(MAC_POINT newPosition);

        public void MoveMacMouseTo()
        {
            var point = RetrieveUserPoint();
            CGWarpMouseCursorPosition(point);
            Console.WriteLine($"Moving cursor to {point.X}:{point.Y}");
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


        public void LegacyGetMacMouseLocation()
        {
            var point = mouseManager.GetCursorPositon();
            Console.WriteLine($"LEGACY: Pointer Location: X = {point.displayX}, Y = {point.displayY}");
        }

        public void LegacyMoveMacMouseTo()
        {            
            var point = new POINT(RetrieveUserPoint());
            mouseManager.MoveCursor(point);
            Console.WriteLine($"LEGACY: Moving cursor to {point.displayX}:{point.displayY}");            
            
            mouseManager.LeftClick(point);
        }


        private static MAC_POINT RetrieveUserPoint()
        {
            Console.Write("Enter mousse new coord (\"x:y\") : ");
            var coordStr = Console.ReadLine()!;
            var x = float.Parse(coordStr.Split(':')[0]);
            var y = float.Parse(coordStr.Split(":")[1]);
            return new MAC_POINT { X = x, Y = y };
        }
    }
}
