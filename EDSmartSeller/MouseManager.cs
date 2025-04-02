﻿namespace EDSmarteSeller
{
    using System;
    using System.Runtime.InteropServices;

    internal class MouseManager
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;



        // Fonction pour obtenir la position actuelle de la souris
        public static POINT GetMousePosition()
        {
            if (GetCursorPos(out POINT point))
            {
                return point;
            }
            throw new Exception("Impossible de récupérer la position de la souris.");
        }

        // Fonction pour déplacer la souris vers des coordonnées spécifiées
        public static void MoveMouse(POINT location)
        {
            if (!SetCursorPos(location.X, location.Y))
            {
                throw new Exception("Impossible de déplacer la souris.");
            }
        }

        // Fonction pour effectuer un clic gauche
        public static void LeftClick(POINT point)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN , point.X, point.Y, 0, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
        }

        /// <summary>
        /// Stay clicked for the given time
        /// </summary>
        /// <param name="point">Where to click</param>
        /// <param name="stayPushMs">Time to stay on decrease in milliseconds</param>
        public static void LeftClick(POINT point, int stayPushMs)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
            Thread.Sleep(stayPushMs);
            mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
        }

    }

}
