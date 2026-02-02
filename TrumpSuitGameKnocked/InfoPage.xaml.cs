

namespace TrumpSuitGameKnocked;

public partial class InfoPage : ContentPage
{
    public readonly Uri uri = new Uri("https://github.com/GiulianoSpaghetti/TrumpSuitGameKnocked");

    private string s = "";
    public InfoPage()
	{
        InitializeComponent();
        Title = $"{App.Dictionary["Informazioni"]}";
        TranslatorCredit.Text = $"Tranlsator: {App.Dictionary["Autore"]}";
        if (App.Dictionary["Revisore"].ToString().Trim() != "")
            RevisorCredit.Text = $"Revisor: {App.Dictionary["Revisore"]}";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }

    protected override void OnAppearing()
    {
        String mazzo, s = "Card images are the property of Modiano", s1 = "";
        base.OnAppearing();
        mazzo = Preferences.Get("mazzo", "Napoletano");
        switch (mazzo)
        {
            case "Siciliano":
                s1 = new StreamReader(FileSystem.OpenAppPackageFileAsync("Mazzi/Siciliano/credits.txt").Result).ReadToEnd();
                s1 = $", {s1}";
                break;
            case "Trevigiano":
                s1 = new StreamReader(FileSystem.OpenAppPackageFileAsync("Mazzi/Trevigiano/credits.txt").Result).ReadToEnd();
                s1 = $", {s1}";
                break;
        }
        Credits.Text = $"{s}{s1} .NET and MAUI are properties of Microsoft Corporation";
    }

}
