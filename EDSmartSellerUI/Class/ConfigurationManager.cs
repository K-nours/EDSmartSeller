namespace EDSmartSellerUI;

using Newtonsoft.Json;
using EDSS_Core;


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


    public async Task<EDSmartSellerParameters> ResetConfig( Editor editor)
    {
        EDSmartSellerParameters eDSmartSellerParameters = new EDSmartSellerParameters();
        editor.Text += AddLine("Demarrage Caliabration...");

        editor.Text += AddLine("Etape 1 Deplacer la sourie sur la ligne de la ressource a vendre et appuyer sur une touche");
        await Task.Run(WaitForKeyPress);

        eDSmartSellerParameters.SelectResourceLocation = _mouseOperations.GetCursorPositon();
        editor.Text += AddLine($"Position save for select resource {eDSmartSellerParameters.SelectResourceLocation.X}:{eDSmartSellerParameters.SelectResourceLocation.Y}");

        //Console.WriteLine("Etape 2 deplacer la sourie sur le bouton quantité \"-\" et appuyer sur une touche");
        //Console.ReadKey();
        eDSmartSellerParameters.DecreaseResourceLocation = _mouseOperations.GetCursorPositon();
        //DisplayInfo($"Position save for decrease Reseource {eDSmartSellerParameters.DecreaseResourceLocation.X}:{eDSmartSellerParameters.DecreaseResourceLocation.Y}");
        
        //Console.WriteLine("Etape 3 deplacer la souri sur le bouton de quantité \"+\" et appuyer sur une touche");
        //Console.ReadKey();
        eDSmartSellerParameters.IncreaseResourceLocation = _mouseOperations.GetCursorPositon();
        //DisplayInfo($"Position save for increase resource {eDSmartSellerParameters.IncreaseResourceLocation.X}:{eDSmartSellerParameters.IncreaseResourceLocation.Y}");
        
        //Console.WriteLine("Etape 4 deplacer la souri sur le bouton de vente et appuyer sur une touche");
        //Console.ReadKey();
        eDSmartSellerParameters.SellPosition = _mouseOperations.GetCursorPositon();
        //DisplayInfo($"Position of sell button {eDSmartSellerParameters.SellPosition.X}:{eDSmartSellerParameters.SellPosition.Y}");

        return eDSmartSellerParameters;

    }

    public void HandleKeyPressedEvent(object sender, EventArgs e)
    {
        _keyPress = true;
    }

    private void DisplayInfo(string text)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[INFO] {text}");
        Console.ResetColor();
    }

    private string AddLine(string text)
    {
        return text+"\n";
    }

    private void WaitForKeyPress()
    {
        while (!_keyPress)
        {
            Thread.Sleep(100);
        }

        _keyPress= false;
    }
}
