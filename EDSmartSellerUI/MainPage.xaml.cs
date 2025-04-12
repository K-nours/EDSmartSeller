namespace EDSmartSellerUI;

using EDSmartSellerUI.Events;
using EDSS_Core;
using EDSS_Core.Enum;
using EDSS_Core.MousseOperations;

public partial class MainPage : ContentPage
{

    public delegate void KeyPressEventHandler(object sender, EventArgs e);
    private IMouseOperations _mouseOperations;
    private ConfigurationManager _configurationManager;
    private event KeyPressEventHandler _keyPressEventHandler;
    private EDSmartSellerParameters? _appParameters;
    public bool IsSelltButtonActive
    {
        get
        {
            return _isSellButtonActive
                && !string.IsNullOrEmpty(QuantityInput.Text)
                && !string.IsNullOrEmpty(WaitTimeInput.Text)
                && !string.IsNullOrEmpty(ExtraPauseTimeInput.Text);
        }
    }

    private bool _isSellButtonActive = false;
    private bool _isCalibarationDone = false;


    public MainPage(IMouseOperations mouseOperations)
    {
        InitializeComponent();
        _mouseOperations = mouseOperations;
        _configurationManager = new ConfigurationManager(_mouseOperations);
        _keyPressEventHandler += _configurationManager.HandleKeyPressedEvent;
        MessageEvent.AddMessage += OnAddMessage;

        _appParameters = _configurationManager.LoadConfiguration();
        if (_appParameters != null)
        {            
            WaitTimeInput.Text = _appParameters.WaitTime.ToString();
            ExtraPauseTimeInput.Text = _appParameters.ExtraPauseTime.ToString();
            setCalibrate(_appParameters.CalibrationPoints != null);
        }
    }


    private void ValidateNumericValue(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var newValue = e.NewTextValue;
        if (!string.IsNullOrEmpty(newValue) && !int.TryParse(newValue, out _))
        {
            // Rétablir la valeur précédente si l'entrée n'est pas un nombre
            entry!.Text = e.OldTextValue;
        }
    }

    private async void OnCalibrate(object sender, EventArgs e)
    {
        validatePosition.IsVisible = true;
        var calibrationPoints = await _configurationManager.ResetCalibrationConfig();
        if (_appParameters != null)
        {
            _appParameters.CalibrationPoints = calibrationPoints;
        }
        else
        {
            _appParameters = new EDSmartSellerParameters();
            _appParameters.CalibrationPoints = calibrationPoints;
        }
        _configurationManager.SaveConfiguration(_appParameters);
        setCalibrate(true);
        validatePosition.IsVisible = false;
    }

    private void setCalibrate(bool value)
    {
        _isCalibarationDone = value;
    }


    private async void ScrollToEnd(object sender, EventArgs e)
    {
        await DisplayInfoSroll.ScrollToAsync(0, DisplayInfoSroll.ContentSize.Height, true);
    }

    private void OnRegisterPosition(object sender, EventArgs e)
    {
        validatePosition.BackgroundColor = Color.FromArgb("#00ff00");
        _keyPressEventHandler.Invoke(this, e);
        validatePosition.Text = string.Empty;
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
        var text = new Label { Text = message, TextColor = color };
        DisplayInfo.Add(text);
    }

}


