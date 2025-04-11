namespace EDSS_Core;

public static class EDSS_Tools
{
    public static void WriteDebug(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(msg);
        Console.ResetColor();
    }
}
