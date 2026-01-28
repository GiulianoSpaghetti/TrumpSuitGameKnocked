namespace TrumpSuitGameKnocked;

public partial class AppShell : Shell
{


    public static Boolean aggiorna = false;
    public AppShell()
    {
    InitializeComponent();
        scapplicazione.Title=App.Dictionary["Applicazione"] as string;
        scopzioni.Title=App.Dictionary["Opzioni"] as string;
        scinformazioni.Title=App.Dictionary["Informazioni"] as string;
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
{
    string current = args.Current.Location.ToString();
    if (current is "//Main")
        if (aggiorna)
        {
            MainPage.MainPageInstance.AggiornaOpzioni();
            aggiorna = false;
        }
    base.OnNavigated(args);

}
}
