namespace EDSS_Core;

using EDSS_Core.Enum;
using EDSS_Core.MousseOperations;

public abstract class ComoditySellersCore(EDSmartSellerParameters parameters, IMouseOperations mouseMnager)
{
    private readonly POINT _selectResourceLocation = parameters.CalibrationPoints.SelectResourceLocation;
    private readonly POINT _decreaseResourceLocation = parameters.CalibrationPoints.DecreaseResourceLocation;
    private readonly POINT _increaseResourceLocation = parameters.CalibrationPoints.IncreaseResourceLocation;
    private readonly POINT _sellPosition = parameters.CalibrationPoints.SellPosition;

    private readonly int ACTION_DELAY = 500;
    private readonly int NB_LOOP_BEFORE_EXT_PAUSE = 10;

    private enum SellMethode
    {
        Byclick,
        StayPush
    }

    protected abstract void DisplayMessage(string message, MessageType type = MessageType.Default);
    protected abstract void WaitForKeyPress();

    public async Task Sell(int initialQuantity, float waitTime, float extraPause = 5)
    {
        var quantityTodecrease = initialQuantity - 1;
        var loopExtrPause = 0;
        var startTime = DateTime.Now;
        var remainingQty = initialQuantity;
        for (int i = 1; i <= initialQuantity; i++)
        {
            remainingQty--;
            DisplayMessage($"Sells comodities {i} of {initialQuantity} - Qty remainig after sell should be  : {remainingQty}");
            mouseMnager.MoveCursor(_selectResourceLocation);
            mouseMnager.LeftClick(_selectResourceLocation);
            Thread.Sleep(ACTION_DELAY);

            var sellMod = ComputeSellMode(quantityTodecrease);
            await CheckCursorPosition(_selectResourceLocation);
            switch (sellMod)
            {
                case SellMethode.Byclick:
                    await SellByClick(quantityTodecrease);
                    break;
                case SellMethode.StayPush:
                    await StayPush(quantityTodecrease);
                    break;
            }

            mouseMnager.MoveCursor(_sellPosition);
            Thread.Sleep(ACTION_DELAY);
            mouseMnager.LeftClick(_sellPosition);

            quantityTodecrease--;

            if (loopExtrPause < NB_LOOP_BEFORE_EXT_PAUSE)
            {
                DisplayMessage($"Pause {waitTime}s");
                Thread.Sleep((int)waitTime * 1000);
                loopExtrPause++;
            }
            else
            {
                var elaspedTime = DateTime.Now - startTime;
                var elaspedDateTime = new DateTime().Add(elaspedTime);
                var eta = new DateTime().AddSeconds(elaspedTime.TotalSeconds * remainingQty / i);

                DisplayMessage($"Extra Pause  {extraPause}s", MessageType.Warning);
                DisplayMessage($"Elasped time :  {elaspedDateTime:HH:mm:ss} Estimed time remaining : {eta:HH:mm:ss}", MessageType.Warning);
                Thread.Sleep((int)extraPause * 1000);

                loopExtrPause = 0;
            }
            await CheckCursorPosition(_sellPosition);
        }

        DisplayMessage("Finish", MessageType.Success);
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

    private async Task StayPush(int quantity)
    {
        int PushDelay = (quantity * 10) + ACTION_DELAY;

        if (PushDelay <= 2500)
        {
            PushDelay = 2500;
        }

        mouseMnager.MoveCursor(_decreaseResourceLocation);
        mouseMnager.LeftClick(_decreaseResourceLocation, PushDelay);
        Thread.Sleep(ACTION_DELAY);

        await CheckCursorPosition(_decreaseResourceLocation);
        mouseMnager.MoveCursor(_increaseResourceLocation);
        mouseMnager.LeftClick(_increaseResourceLocation);
        Thread.Sleep(ACTION_DELAY);
    }

    private async Task SellByClick(int quantity)
    {
        mouseMnager.MoveCursor(_decreaseResourceLocation);
        for (int j = 0; j <= quantity; j++)
        {
            mouseMnager.LeftClick(_decreaseResourceLocation);
            Thread.Sleep(15);
            await CheckCursorPosition(_decreaseResourceLocation);
        }

        mouseMnager.MoveCursor(_increaseResourceLocation);
        mouseMnager.LeftClick(_increaseResourceLocation);
        Thread.Sleep(ACTION_DELAY);
        await CheckCursorPosition(_increaseResourceLocation);
    }

    private async Task CheckCursorPosition(POINT location)
    {
        var curretPosition = mouseMnager.GetCursorPositon();
        if (!curretPosition.IsEqualTo(location))
        {
            DisplayMessage("PAUSE : mouse  have been moved by the user. Press any key to resume...", MessageType.Info);
            await Task.Run(WaitForKeyPress);
            DisplayMessage("Resuming...", MessageType.Success);

            for (int i = 3; i > 0; i--)
            {
                DisplayMessage(i.ToString());
                Thread.Sleep(1000);
            }
        }
    }
}

