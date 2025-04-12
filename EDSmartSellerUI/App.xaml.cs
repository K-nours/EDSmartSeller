namespace EDSmartSellerUI
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window= new Window(new AppShell());
            // Définir la taille et la position de la fenêtre
            //window.Width = 800;  // Largeur en pixels
            //window.Height = 600; // Hauteur en pixels
            //window.X = 100;      // Position X sur l'écran
            //window.Y = 50;       // Position Y sur l'écran
            return window;
        }
    }
}