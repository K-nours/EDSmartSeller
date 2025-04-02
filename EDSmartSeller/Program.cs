// See https://aka.ms/new-console-template for more information
using EDSmarteSeller;
using System.Reflection;

var configMgr = new ConfigurationManager();
Assembly assembly = Assembly.GetExecutingAssembly();
AssemblyName assemblyName = assembly.GetName();
Console.WriteLine($"Starting Elite Dangerous : Smart Seller (V{assemblyName.Version})");
var edParams = configMgr.LoadConfiguration();
if (edParams != null)
{
    Console.WriteLine("Utiliser la configuration engerister? [(default)O/N] : ");
    var response = Console.ReadKey().KeyChar;

    if (char.ToUpper(response) == 'N')
    {
        edParams = null;
    }
}


if (edParams == null)
{
    edParams = configMgr.ResetConfig();
    configMgr.SaveConfiguration(edParams);
}

Console.WriteLine();


var restart = true;
var sellManeger = new ComoditySellers(edParams);
while (restart)
{
    int initialQuantity = 0;
    Console.Write("Quantité de ressource initial a vendre : ");
    while (!int.TryParse(Console.ReadLine(), out initialQuantity))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Valeur non valide: Entre un nombre entier");
        Console.ResetColor();
    }

    Console.Write("Temps entre 2 cyle (en seconde, ex: 1,5) : ");
    float waitTimeSeconde = 3;

    while (!float.TryParse(Console.ReadLine()?.Replace(".", ","), out waitTimeSeconde))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Valeur non valide: Entre un nombre entier, out float EX: 1,5");
        Console.ResetColor();
    }

    Console.WriteLine("Appuyer sur un touche pour commencer la vente");
    Console.ReadKey();
    var startTime = DateTime.Now;
    Console.WriteLine("Demarrage dans 3s");
    Thread.Sleep(3000);
    sellManeger.Sell(initialQuantity, waitTimeSeconde);

    Console.ForegroundColor = ConsoleColor.Green;
    var totalTime = DateTime.Now - startTime;

    Console.WriteLine($"Vente terminé en {totalTime.Hours}:{totalTime.Minutes}:{totalTime.Seconds}");
    Console.ResetColor();

    Console.Write("Recommencer une Vente ? [O/N] : ");
    var response = Console.ReadKey();
    if (char.ToUpper(response.KeyChar) == 'O')
    {
        restart = true;
        Console.Clear();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("Exiting program...");
        restart = false;
    }
}



