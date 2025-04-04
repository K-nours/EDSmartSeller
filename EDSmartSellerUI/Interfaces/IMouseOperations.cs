namespace EDSmartSellerUI;
using EDSS_Core;

public interface IMouseOperations
{
    POINT GetCursorPositon();
    void MoveCursor(POINT location);
    void LeftClick(POINT location);
    void LeftClick(POINT point, int stayPushMs);
   
}

