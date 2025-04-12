namespace EDSmartSeller;

using EDSS_Core;
using EDSS_Core.Enum;
using EDSS_Core.MousseOperations;

internal class ComoditySellers(EDSmartSellerParameters parameters, IMouseOperations mouseMnager) : ComoditySellersCore(parameters, mouseMnager)
{
    protected override void DisplayMessage(string message, MessageType type = MessageType.Default)
    {
        switch (type)
        {
            case MessageType.Info:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case MessageType.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case MessageType.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            default:
                break;
        }
        Console.WriteLine(message);
        Console.ResetColor();
    }

    protected override void WaitForKeyPress()
    {
        Console.ReadKey();
    }


}


   
