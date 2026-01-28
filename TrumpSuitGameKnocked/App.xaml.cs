using System.Globalization;

namespace TrumpSuitGameKnocked;

public partial class App : Application
{
    public static ResourceDictionary Dictionary { get; private set; }
    public static readonly CancellationTokenSource cancellationTokenSource= new CancellationTokenSource();
    public static string Piattaforma { get; private set; }
    public App()
    {
        InitializeComponent();
        try
        {
            Dictionary = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            Dictionary = Resources["en"] as ResourceDictionary;
        }
        Piattaforma = DeviceInfo.Current.Model;
        if (Piattaforma == "System Product Name")
            Piattaforma = "Windows " + DeviceInfo.Current.VersionString;
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