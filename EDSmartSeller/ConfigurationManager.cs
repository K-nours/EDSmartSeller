namespace EDSmartSeller
{
    using EDSS_Core;
    using EDSS_Core.Enum;
    using EDSS_Core.MousseOperations;
    using Newtonsoft.Json;

    internal class ConfigurationManager
    {
        private const string win_saveFile = ".\\EDSSconfig.json";
        private const string mac_saveFile = "./EDSSconfig.json"; // Enregistre à la racine du dossier utilisateur. Voir comment recupérer le contexte d'execution
        private readonly string _saveFile;
        private readonly IMouseOperations _mouseOperations;

        public ConfigurationManager(IMouseOperations mouseOperations, EnvironementTarget target)
        {
            _mouseOperations = mouseOperations;
            _saveFile = "unknow";
            switch (target)
            {
                case EnvironementTarget.Win:
                    _saveFile= win_saveFile;
                    break;
                case EnvironementTarget.Mac:
                    _saveFile= mac_saveFile;
                    break;
            }
        }

        public void SaveConfiguration(EDSmartSellerParameters parameters)
        {
            File.WriteAllText(_saveFile, JsonConvert.SerializeObject(parameters));
        }

        public EDSmartSellerParameters? LoadConfiguration()
        {
            if (File.Exists(_saveFile))
            {
                Console.WriteLine();
                var data = File.ReadAllText(_saveFile);
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
            eDSmartSellerParameters.SelectResourceLocation = _mouseOperations.GetCursorPositon();
            DisplayInfo($"Position save for select resource {eDSmartSellerParameters.SelectResourceLocation.win_x}:{eDSmartSellerParameters.SelectResourceLocation.win_y}");
            Console.WriteLine("Etape 2 deplacer la sourie sur le bouton quantité \"-\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.DecreaseResourceLocation = _mouseOperations.GetCursorPositon();
            DisplayInfo($"Position save for decrease Reseource {eDSmartSellerParameters.DecreaseResourceLocation.win_x}:{eDSmartSellerParameters.DecreaseResourceLocation.win_y}");

            Console.WriteLine("Etape 3 deplacer la souri sur le bouton de quantité \"+\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.IncreaseResourceLocation = _mouseOperations.GetCursorPositon();
            DisplayInfo($"Position save for increase resource {eDSmartSellerParameters.IncreaseResourceLocation.win_x}:{eDSmartSellerParameters.IncreaseResourceLocation.win_y}");

            Console.WriteLine("Etape 4 deplacer la souri sur le bouton de vente et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.SellPosition = _mouseOperations.GetCursorPositon();
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
