namespace EDSmartSellerUI;

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
        //ConsoleLog.Text = "Hello world";
        //validatePosition.IsVisible = true;
        //await _configurationManager.ResetConfig( ConsoleLog);
        //validatePosition.IsVisible = false;
        var text = new Label { Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut interdum, nisl vel tincidunt congue, neque eros pharetra augue, sit amet euismod elit ante quis purus. Sed luctus urna nec ultricies maximus. Ut id accumsan dui. Morbi et aliquam odio, vel laoreet odio. Donec finibus aliquam ultrices. Praesent et imperdiet eros, sit amet tempus diam. Nulla facilisi.\r\n\r\nAenean id turpis velit. Nullam tempor faucibus massa, luctus ornare elit fermentum vitae. Curabitur a vehicula mi. Pellentesque vitae ullamcorper est, in dignissim enim. Nulla aliquam convallis lorem in maximus. Pellentesque quis mi a mauris vulputate convallis eu ac velit. Donec lacus leo, auctor et ante quis, egestas elementum magna. Suspendisse potenti. Nulla sollicitudin rutrum sapien, sit amet tristique ligula pretium id. Nunc interdum est auctor tortor varius, semper rhoncus neque mollis. Fusce placerat metus vitae luctus vulputate. Pellentesque posuere pharetra enim, ut consequat eros. Ut lectus dui, dapibus sed ex vitae, iaculis faucibus erat. Nunc consequat sit amet tellus quis hendrerit.\r\n\r\nMorbi vel viverra libero. Vestibulum ut molestie nulla, non lacinia ante. Cras mollis urna nibh, eu auctor urna ultricies in. Aliquam egestas, ipsum id efficitur suscipit, magna leo interdum risus, sed tincidunt elit mauris vel purus. Curabitur sodales ante et sapien commodo blandit. Praesent id posuere felis, eu tincidunt nunc. Aenean tempor malesuada lectus in fringilla. Vivamus dictum eleifend velit ultrices pharetra. Nam sit amet mollis libero. Aliquam quis lobortis erat, id malesuada dolor. Nulla mattis arcu neque, a faucibus orci vestibulum ut. Proin porttitor non nisl ac aliquet.\r\n\r\nVestibulum quis tortor lacinia, sagittis turpis a, finibus leo. Fusce rutrum massa nec magna accumsan condimentum. Fusce ut tincidunt ipsum, imperdiet auctor arcu. Ut ac augue euismod, pretium risus ac, venenatis sapien. Curabitur ut mollis est. Curabitur leo libero, lobortis vitae nulla ac, rhoncus tempus nibh. Aenean sollicitudin suscipit lacus vitae accumsan. Sed blandit dolor non pulvinar vestibulum. Suspendisse ut luctus urna. Nunc ut metus interdum dolor tincidunt volutpat ut at mauris. Pellentesque et lacus id nunc placerat semper. Praesent et ex sed nulla rhoncus vestibulum.\r\n\r\nUt tristique augue et porta congue. Maecenas sagittis maximus ullamcorper. Maecenas eget pulvinar sapien. Sed convallis ultricies leo hendrerit commodo. Nullam congue justo est, id ultrices lorem accumsan eget. Nunc lorem massa, bibendum eu enim sit amet, sollicitudin rutrum dui. Nam et est laoreet, accumsan nisl eu, lobortis massa. Maecenas venenatis et enim volutpat pulvinar. Curabitur tincidunt rhoncus tellus, ac tincidunt nisi blandit id. Maecenas sodales orci consequat, dictum libero vitae, posuere ipsum. Phasellus id dolor dui. Cras non sodales dui. Nullam blandit tincidunt est sit amet placerat.", TextColor = Color.FromRgba("#060270") };
        var selectableText = new Editor { Text = "WTF", TextColor = Color.FromRgba("#FE9900"), IsReadOnly=true,  Margin=0, AutoSize=EditorAutoSizeOption.TextChanges};
        DisplayInfo.Add(text);
       

        //   DisplayInfo.Add(selectableText);
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

  
}


