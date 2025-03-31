namespace EDSmartSellerUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    private void ValidateNumericValue(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var newValue = e.NewTextValue;
        if ( !string.IsNullOrEmpty(newValue) && !int.TryParse(newValue, out _))
        {
            // Rétablir la valeur précédente si l'entrée n'est pas un nombre
            entry!.Text = e.OldTextValue;
        }
    }

    private void OnCalibrate(object sender, EventArgs e)
    {

    }
}


