namespace EDSmartSellerUI;

using EDSmartSellerUI.Enum;
using EDSmartSellerUI.Events;
using EDSS_Core.MousseOperations;

public partial class MainPage : ContentPage
{

    public delegate void KeyPressEventHandler(object sender, EventArgs e);
    private IMouseOperations _mouseOperations;
    private ConfigurationManager _configurationManager;
    private event KeyPressEventHandler _keyPressEventHandler;


    public MainPage(IMouseOperations mouseOperations)
    {
        InitializeComponent();
        _mouseOperations = mouseOperations;
        _configurationManager=new ConfigurationManager(_mouseOperations);
        _keyPressEventHandler += _configurationManager.HandleKeyPressedEvent;
        MessageEvent.AddMessage += OnAddMessage;
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

    private async void OnCalibrate(object sender, EventArgs e)
    {
        validatePosition.IsVisible = true;
        await _configurationManager.ResetConfig( );
        validatePosition.IsVisible = false;
    }

    private async void ScrollToEnd(object sender, EventArgs e)
    {
        await DisplayInfoSroll.ScrollToAsync(0, DisplayInfoSroll.ContentSize.Height, true);
    }

    private void OnRegisterPosition(object sender, EventArgs e)
    {
        validatePosition.BackgroundColor = Color.FromArgb("#00ff00");
        _keyPressEventHandler.Invoke(this, e);
        validatePosition.Text=string.Empty;
        validatePosition.BackgroundColor = Color.FromArgb("#ff0000");
    }

    private void OnAddMessage(object sender, string message, MessageType messageType)
    {
        var color = new Color();
        switch (messageType)
        {
            case MessageType.Default:
                color = Color.FromArgb("#000000");
                break;
            case MessageType.Info:
                color = Color.FromArgb("#060270");
                break;
            case MessageType.Warning:
                color = Color.FromArgb("#FFDE59");
                break;
            case MessageType.Success:
                color = Color.FromArgb("#7DDA58");
                break;
            case MessageType.Error:
                color = Color.FromArgb("#E4080A");
                break;
        }
        var text = new Label { Text = message, TextColor = color};
        DisplayInfo.Add(text);
    }

}


