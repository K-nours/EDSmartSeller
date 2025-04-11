namespace EDSmarteSeller;
using EDSS_Core;
using EDSS_Core.MousseOperations;

internal class ComoditySellers(EDSmartSellerParameters parameters, IMouseOperations mouseMnager)
{
    private readonly POINT _selectResourceLocation = parameters.SelectResourceLocation;
    private readonly POINT _decreaseResourceLocation = parameters.DecreaseResourceLocation;
    private readonly POINT _increaseResourceLocation = parameters.IncreaseResourceLocation;
    private readonly POINT _sellPosition = parameters.SellPosition;

    private readonly int ACTION_DELAY = 500;
    private readonly int NB_LOOP_BEFORE_EXT_PAUSE = 10;

    public enum SellMethode
    {
        Byclick,
        StayPush
    }

    public void Sell(int initialQuantity, float waitTime, float extraPause = 5)
    {
        var quantityTodecrease = initialQuantity - 1;
        var loopExtrPause = 0;
        var startTime = DateTime.Now;
        var remainingQty = initialQuantity;
        for (int i = 1; i <= initialQuantity; i++)
        {
            remainingQty--;
            Console.WriteLine($"Sells comodities {i} of {initialQuantity} - Qty remainig after sell should be  : {remainingQty}");
            mouseMnager.MoveCursor(_selectResourceLocation);
            mouseMnager.LeftClick(_selectResourceLocation);
            Thread.Sleep(ACTION_DELAY);

            var sellMod = ComputeSellMode(quantityTodecrease);

            switch (sellMod)
            {
                case SellMethode.Byclick:
                    SellByClick(quantityTodecrease);
                    break;
                case SellMethode.StayPush:
                    StayPush(quantityTodecrease);
                    break;
            }

            mouseMnager.MoveCursor(_sellPosition);
            Thread.Sleep(ACTION_DELAY);
            mouseMnager.LeftClick(_selectResourceLocation);

            quantityTodecrease--;

            if (loopExtrPause < NB_LOOP_BEFORE_EXT_PAUSE)
            {
                Console.WriteLine($"Pause {waitTime}s");
                Thread.Sleep((int)waitTime * 1000);
                loopExtrPause++;
            }
            else
            {
                var elaspedTime = DateTime.Now - startTime;
                var elaspedDateTime = new DateTime().Add(elaspedTime);
                var eta = new DateTime().AddSeconds(elaspedTime.TotalSeconds * remainingQty / i);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Extra Pause  {extraPause}s");
                Console.WriteLine($"Elasped time :  {elaspedDateTime:HH:mm:ss} Estimed time remaining : {eta:HH:mm:ss}");
                Thread.Sleep((int)extraPause * 1000);
                Console.ResetColor();
                loopExtrPause = 0;
            }
        }

        Console.WriteLine("Finish");
    }


    private static SellMethode ComputeSellMode(int quantity)
    {
        if (quantity > 50)
        {
            return SellMethode.StayPush;
        }
        else
        {
            return SellMethode.Byclick;
        }
    }

    private void StayPush(int quantity)
    {
        int PushDelay = (quantity * 10) + ACTION_DELAY;

        if (PushDelay <= 2500)
        {
            PushDelay = 2500;
        }

        mouseMnager.MoveCursor(_decreaseResourceLocation);
        mouseMnager.LeftClick(_decreaseResourceLocation, PushDelay);
        Thread.Sleep(ACTION_DELAY);

        mouseMnager.MoveCursor(_increaseResourceLocation);
        mouseMnager.LeftClick(_increaseResourceLocation);
        Thread.Sleep(ACTION_DELAY);
    }

    private void SellByClick(int quantity)
    {
        mouseMnager.MoveCursor(_decreaseResourceLocation);
        for (int j = 0; j <= quantity; j++)
        {
            mouseMnager.LeftClick(_selectResourceLocation);
            Thread.Sleep(15);
        }
        mouseMnager.MoveCursor(_increaseResourceLocation);
        mouseMnager.LeftClick(_increaseResourceLocation);
        Thread.Sleep(ACTION_DELAY);
    }
}
