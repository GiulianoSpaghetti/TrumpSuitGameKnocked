namespace TrumpSuitGameKnocked.maui;

public partial class AppShell : Shell
{


    public static Boolean aggiorna = false;
    public AppShell()
    {
    InitializeComponent();
        scapplicazione.Title=App.d["Applicazione"] as string;
        scopzioni.Title=App.d["Opzioni"] as string;
        scinformazioni.Title=App.d["Informazioni"] as string;
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
{
    string current = args.Current.Location.ToString();
    if (current is "//Main")
        if (aggiorna)
        {
            MainPage.main.AggiornaOpzioni();
            aggiorna = false;
        }
    base.OnNavigated(args);

}
}
