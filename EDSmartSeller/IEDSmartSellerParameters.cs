
namespace EDSmarteSeller
{
    internal interface IEDSmartSellerParameters
    {

         POINT SelectResourceLocation { get; set; }
         POINT DecreaseResourceLocation { get; set; }
         POINT IncreaseResourceLocation { get; set; }
         POINT SellPosition { get; set; }
    }
}
