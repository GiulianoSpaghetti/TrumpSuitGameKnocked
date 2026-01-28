

namespace TrumpSuitGameKnocked;

public partial class InfoPage : ContentPage
{
    public readonly Uri uri = new Uri("https://github.com/GiulianoSpaghetti/TrumpSuitGameKnocked");

    private string s = "";
    public InfoPage()
	{
        InitializeComponent();
        Title = $"{App.Dictionary["Informazioni"]}";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Preferences.Get("mazzo", "Napoletano")=="Siciliano")
        {
            s = new StreamReader(FileSystem.OpenAppPackageFileAsync("Mazzi/Siciliano/credits.txt").Result).ReadToEnd();
            s=$", {s}";
        }
        else
            s = "";

        Credits.Text = $"Card images are the property of Modiano{s}, .NET and MAUI are properties of Microsoft Corporation";
    }

}
