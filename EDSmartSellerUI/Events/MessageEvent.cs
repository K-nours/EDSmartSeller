namespace EDSmartSellerUI.Events;

using EDSS_Core.Enum;

public static class MessageEvent{
    public delegate void AddMessageEventHandler(object sender, string message,MessageType messageType);

    // Déclaration de l'événement
    public static event AddMessageEventHandler? AddMessage;

    // Méthode pour invoquer l'événement
    public static void RaiseEvent(object sender, string message, MessageType messageType = MessageType.Default)
    {
        AddMessage?.Invoke(sender,message, messageType);
    }

}