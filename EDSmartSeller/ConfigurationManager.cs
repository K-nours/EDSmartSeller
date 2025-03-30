namespace EDSmarteSeller
{
    using Newtonsoft.Json;


    internal class ConfigurationManager
    {
        private readonly string saveFile = ".\\EDSSconfig.json";

        public void SaveConfiguration(EDSmartSellerParameters parameters)
        {
            File.WriteAllText(saveFile,JsonConvert.SerializeObject(parameters));
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


        public EDSmartSellerParameters ResetConfig()
        {
            EDSmartSellerParameters eDSmartSellerParameters = new EDSmartSellerParameters();
            Console.WriteLine("Demarrage Caliabration...");
            Console.WriteLine("Etape 1 Deplacer la sourie sur la ligne de la ressource a vendre et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.SelectResourceLocation = MouseManager.GetMousePosition();
            DisplayInfo($"Position save for select resource {eDSmartSellerParameters.SelectResourceLocation.X}:{eDSmartSellerParameters.SelectResourceLocation.Y}");
            Console.WriteLine("Etape 2 deplacer la sourie sur le bouton quantité \"-\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.DecreaseResourceLocation = MouseManager.GetMousePosition();
            DisplayInfo($"Position save for decrease Reseource {eDSmartSellerParameters.DecreaseResourceLocation.X}:{eDSmartSellerParameters.DecreaseResourceLocation.Y}");

            Console.WriteLine("Etape 3 deplacer la souri sur le bouton de quantité \"+\" et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.IncreaseResourceLocation = MouseManager.GetMousePosition();
            DisplayInfo($"Position save for increase resource {eDSmartSellerParameters.IncreaseResourceLocation.X}:{eDSmartSellerParameters.IncreaseResourceLocation.Y}");

            Console.WriteLine("Etape 4 deplacer la souri sur le bouton de vente et appuyer sur une touche");
            Console.ReadKey();
            eDSmartSellerParameters.SellPosition = MouseManager.GetMousePosition();
            DisplayInfo($"Position of sell button {eDSmartSellerParameters.SellPosition.X}:{eDSmartSellerParameters.SellPosition.Y}");

            return eDSmartSellerParameters;

        }

        private void DisplayInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[INFO] {text}");
            Console.ResetColor();
        }
    }
}
