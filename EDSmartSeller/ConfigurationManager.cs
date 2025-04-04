namespace EDSmarteSeller
{
    using Newtonsoft.Json;
    using EDSS_Core;
    using EDSS_Core.MousseOperations;

    internal class ConfigurationManager(IMouseOperations mouseManager)
    {
        private readonly string saveFile = ".\\EDSSconfig.json";

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


        public  EDSmartSellerParameters ResetConfig()
        {
            EDSmartSellerParameters eDSmartSellerParameters = new();
            Console.WriteLine("Demarrage Caliabration...");
            Console.WriteLine("Etape 1 Deplacer la sourie sur la ligne de la ressource a vendre et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.SelectResourceLocation = mouseManager.GetCursorPositon();
            DisplayInfo($"Position save for select resource {eDSmartSellerParameters.SelectResourceLocation.win_x}:{eDSmartSellerParameters.SelectResourceLocation.win_y}");
            Console.WriteLine("Etape 2 deplacer la sourie sur le bouton quantité \"-\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.DecreaseResourceLocation = mouseManager.GetCursorPositon();
            DisplayInfo($"Position save for decrease Reseource {eDSmartSellerParameters.DecreaseResourceLocation.win_x}:{eDSmartSellerParameters.DecreaseResourceLocation.win_y}");

            Console.WriteLine("Etape 3 deplacer la souri sur le bouton de quantité \"+\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.IncreaseResourceLocation = mouseManager.GetCursorPositon();
            DisplayInfo($"Position save for increase resource {eDSmartSellerParameters.IncreaseResourceLocation.win_x}:{eDSmartSellerParameters.IncreaseResourceLocation.win_y}");

            Console.WriteLine("Etape 4 deplacer la souri sur le bouton de vente et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.SellPosition = mouseManager.GetCursorPositon();
            DisplayInfo($"Position of sell button {eDSmartSellerParameters.SellPosition.win_x}:{eDSmartSellerParameters.SellPosition.win_y}");

            return eDSmartSellerParameters;

        }

        private static void DisplayInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[INFO] {text}");
            Console.ResetColor();
        }
    }
}
