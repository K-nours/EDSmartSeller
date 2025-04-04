namespace EDSmarteSeller
{
    internal class ComoditySellers
    {
        private POINT _selectResourceLocation = new POINT();
        private POINT _decreaseResourceLocation = new POINT();
        private POINT _increaseResourceLocation = new POINT();
        private POINT _sellPosition = new POINT();

        private readonly int ACTION_DELAY = 500;
        private readonly int NB_LOOP_BEFORE_EXT_PAUSE = 10;

        public enum SellMethode
        {
            Byclick,
            StayPush
        }

        public ComoditySellers(EDSmartSellerParameters parameters)
        {
            _selectResourceLocation = parameters.SelectResourceLocation;
            _decreaseResourceLocation = parameters.DecreaseResourceLocation;
            _increaseResourceLocation = parameters.IncreaseResourceLocation;
            _sellPosition = parameters.SellPosition;
        }

        public void Sell(int initialQuantity, float waitTime, float extraPause = 5)
        {
            var quantityTodecrease = initialQuantity - 1;
            var loopExtrPause = 0;
            for (int i = 1; i <= initialQuantity; i++)
            {
                Console.WriteLine($"Sells comodities {i} of {initialQuantity}");
                MouseManager.MoveMouse(_selectResourceLocation);
                MouseManager.LeftClick(_selectResourceLocation);
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

                MouseManager.MoveMouse(_sellPosition);
                Thread.Sleep(ACTION_DELAY);
                MouseManager.LeftClick(_selectResourceLocation);

                quantityTodecrease--;

                if (loopExtrPause < NB_LOOP_BEFORE_EXT_PAUSE)
                {
                    Console.WriteLine($"Pause {waitTime}s");
                    Thread.Sleep((int)waitTime * 1000);
                    loopExtrPause++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Extra Pause  {extraPause}s");
                    Thread.Sleep((int)extraPause * 1000);
                    Console.ResetColor();
                    loopExtrPause = 0;
                }
            }

            Console.WriteLine("Finish");
        }


        private SellMethode ComputeSellMode(int quantity)
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

            MouseManager.MoveMouse(_decreaseResourceLocation);
            MouseManager.LeftClick(_decreaseResourceLocation, PushDelay);
            Thread.Sleep(ACTION_DELAY);

            MouseManager.MoveMouse(_increaseResourceLocation);
            MouseManager.LeftClick(_increaseResourceLocation);
            Thread.Sleep(ACTION_DELAY);
        }

        private void SellByClick(int quantity)
        {
            MouseManager.MoveMouse(_decreaseResourceLocation);
            for (int j = 0; j <= quantity; j++)
            {
                MouseManager.LeftClick(_selectResourceLocation);
                Thread.Sleep(15);
            }
            MouseManager.MoveMouse(_increaseResourceLocation);
            MouseManager.LeftClick(_increaseResourceLocation);
            Thread.Sleep(ACTION_DELAY);
        }
    }
}
