using System.Globalization;

namespace TrumpSuitGameKnocked.maui;

public partial class App : Application
{
    private static string piattaforma;
    private static ResourceDictionary dic=null;
    public static ResourceDictionary d
    {
        get => dic;
    }
    public static readonly CancellationTokenSource cancellationTokenSource= new CancellationTokenSource();
    public static string Piattaforma
    {
        get => piattaforma;
    }
    public App()
    {
        InitializeComponent();
        try
        {
            dic = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            dic = Resources["en"] as ResourceDictionary;
        }
        piattaforma = DeviceInfo.Current.Model;
        if (piattaforma == "System Product Name")
            piattaforma = "Windows " + DeviceInfo.Current.VersionString;
    }

#if ANDROID
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
#else        
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShellWindows());
    }
#endif
}