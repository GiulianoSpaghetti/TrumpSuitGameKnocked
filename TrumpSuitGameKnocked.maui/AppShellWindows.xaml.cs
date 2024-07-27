namespace TrumpSuitGameKnocked.maui;

public partial class AppShellWindows : Shell
{
    public static bool aggiorna = false;

    public AppShellWindows()
    {
        InitializeComponent();
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        string current = args.Current.Location.ToString();
        base.OnNavigated(args);
        if (current is "//Main")
            if (aggiorna)
            {
                MainPage.main.AggiornaOpzioni();
                aggiorna = false;
            }

    }
}
