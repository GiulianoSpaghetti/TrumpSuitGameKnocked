namespace TrumpSuitGameKnocked.maui;

public partial class InfoPage : ContentPage
{
    public readonly Uri uri= new Uri("https://github.com/GiulianoSpaghetti/TrumpSuitGameKnocked.maui")

    public InfoPage()
	{
		InitializeComponent();
        Title = $"{App.d["Informazioni"]}";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }

}
