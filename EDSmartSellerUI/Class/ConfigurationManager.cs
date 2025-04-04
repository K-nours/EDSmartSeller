namespace EDSmartSellerUI;

using Newtonsoft.Json;
using EDSS_Core.MousseOperations;
using EDSS_Core;
using EDSmartSellerUI.Enum;
using EDSmartSellerUI.Events;

internal class ConfigurationManager
{
    private readonly string saveFile = ".\\EDSSconfig.json";
    private IMouseOperations _mouseOperations;
    private bool _keyPress = false;



    public ConfigurationManager(IMouseOperations mouseOperations)
    {
        _mouseOperations = mouseOperations;
    }

    public void SaveConfiguration(EDSmartSellerParameters parameters)
    {
        File.WriteAllText(saveFile, JsonConvert.SerializeObject(parameters));
    }

    public EDSmartSellerParameters? LoadConfiguration()
    {
        if (File.Exists(saveFile))
        {
            Console.WriteLine();
            var data = File.ReadAllText(saveFile);
            var param = JsonConvert.DeserializeObject<EDSmartSellerParameters>(data);
            return param;
        }
        return null;
    }


    public async Task<EDSmartSellerParameters> ResetConfig()
    {
        EDSmartSellerParameters eDSmartSellerParameters = new EDSmartSellerParameters();
        DisplayMessage("Demarrage Caliabration...");

        DisplayMessage("Etape 1 Deplacer la sourie sur la ligne de la ressource a vendre et appuyer sur une touche");
        await Task.Run(WaitForKeyPress);

        eDSmartSellerParameters.SelectResourceLocation = _mouseOperations.GetCursorPositon();

        DisplayMessage($"Position save for select resource {eDSmartSellerParameters.SelectResourceLocation.displayX}:{eDSmartSellerParameters.SelectResourceLocation.displayY}", MessageType.Info);

        DisplayMessage("Etape 2 deplacer la sourie sur le bouton quantité \"-\" et appuyer sur une touche");
        await Task.Run(WaitForKeyPress);
        eDSmartSellerParameters.DecreaseResourceLocation = _mouseOperations.GetCursorPositon();
        DisplayMessage($"Position save for decrease Reseource {eDSmartSellerParameters.DecreaseResourceLocation.displayX}:{eDSmartSellerParameters.DecreaseResourceLocation.displayY}", MessageType.Info);

        DisplayMessage("Etape 3 deplacer la souri sur le bouton de quantité \"+\" et appuyer sur une touche");
        await Task.Run(WaitForKeyPress);
        eDSmartSellerParameters.IncreaseResourceLocation = _mouseOperations.GetCursorPositon();
        DisplayMessage($"Position save for increase resource {eDSmartSellerParameters.IncreaseResourceLocation.displayX}:{eDSmartSellerParameters.IncreaseResourceLocation.displayY}", MessageType.Info);

        DisplayMessage("Etape 4 deplacer la souri sur le bouton de vente et appuyer sur une touche");
        await Task.Run(WaitForKeyPress);
        eDSmartSellerParameters.SellPosition = _mouseOperations.GetCursorPositon();
        DisplayMessage($"Position of sell button {eDSmartSellerParameters.SellPosition.displayY}:{eDSmartSellerParameters.SellPosition.displayY}", MessageType.Info);

        return eDSmartSellerParameters;

    }

    public void HandleKeyPressedEvent(object sender, EventArgs e)
    {
        _keyPress = true;
    }

    private static string AddLine(string text)
    {
        return text + "\n";
    }

    private void WaitForKeyPress()
    {
        while (!_keyPress)
        {
            Thread.Sleep(100);
        }

        _keyPress = false;
    }

    private void DisplayMessage(string text, MessageType messageType = MessageType.Default)
    {
        MessageEvent.RaiseEvent(this, text, messageType);
    }
}
