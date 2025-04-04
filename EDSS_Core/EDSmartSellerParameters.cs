namespace EDSS_Core
{
    public class EDSmartSellerParameters
    {
        private POINT _SelectResourceLocation = new();
        private POINT _DecreaseResourceLocation = new();
        private POINT _IncreaseResourceLocation = new();
        private POINT _SellPosition = new();


        public POINT SelectResourceLocation { get => _SelectResourceLocation; set => _SelectResourceLocation = value; }
        public POINT DecreaseResourceLocation { get => _DecreaseResourceLocation; set => _DecreaseResourceLocation = value; }
        public POINT IncreaseResourceLocation { get => _IncreaseResourceLocation; set => _IncreaseResourceLocation = value; }
        public POINT SellPosition { get => _SellPosition; set => _SellPosition = value; }
    }
}
