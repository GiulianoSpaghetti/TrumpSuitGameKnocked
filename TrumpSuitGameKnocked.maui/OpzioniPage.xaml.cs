using CommunityToolkit.Maui.Alerts;
using org.altervista.numerone.framework;
using System.Threading;

namespace TrumpSuitGameKnocked.maui;

public partial class OpzioniPage : ContentPage
{
    private static bool briscolaDaPunti;
    private static bool avvisaTalloneFinito;
    private static UInt16 secondi;
    private static UInt16 livello;
    public OpzioniPage()
    {
        InitializeComponent();
        livello = (UInt16)Preferences.Get("livello", 3);
        txtNomeUtente.Text = Preferences.Get("nomeUtente", "numerone");
        txtCpu.Text = Preferences.Get("nomeCpu", "numerona");
        secondi = (UInt16)Preferences.Get("secondi", 5);
        txtSecondi.Text = secondi.ToString();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        swAvvisaTallone.IsToggled = avvisaTalloneFinito;
        swCartaBriscola.IsToggled = briscolaDaPunti;
        pkrlivello.SelectedIndex = livello - 1;
        Title = $"{App.d["Opzioni"]}";
        opNomeCpu.Text = $"{App.d["NomeCpu"]}: ";
        opNomeUtente.Text= $"{App.d["NomeUtente"]}: ";
        lbSecondi.Text= $"{App.d["secondi"]}";
        lbLivello.Text = $"{App.d["IlLivelloE"]}";
        lbCartaBriscola.Text= $"{App.d["BriscolaDaPunti"]}";
        lbAvvisaTallone.Text= $"{App.d["AvvisaTallone"]}";
        btnOk.Text= $"{App.d["Salva"]}";
    }

    public async void OnOk_Click(Object source, EventArgs evt)
    {
        Preferences.Set("nomeUtente", txtNomeUtente.Text);
        Preferences.Set("nomeCpu", txtCpu.Text);
        briscolaDaPunti = swCartaBriscola.IsToggled;
        Preferences.Set("briscolaDaPunti", briscolaDaPunti);
        avvisaTalloneFinito = swAvvisaTallone.IsToggled;
        Preferences.Set("avvisaTalloneFinito", avvisaTalloneFinito);

        try
        {
            secondi = UInt16.Parse(txtSecondi.Text);
        }
        catch (FormatException ex)
        {
            await Snackbar.Make($"{App.d["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            return;
        }
           catch (OverflowException ex)
        {
            await Snackbar.Make($"{App.d["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            return;
        }
        if (secondi <5 || secondi>20)
        {
            await Snackbar.Make($"{App.d["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            txtSecondi.Text = ((UInt16)Preferences.Get("secondi", 5)).ToString();
            return;
        }
        Preferences.Set("secondi", secondi);
        Preferences.Set("livello", pkrlivello.SelectedIndex + 1);
#if ANDROID
        AppShell.aggiorna = true;
#else
        AppShellWindows.aggiorna = true;
#endif
        await Shell.Current.GoToAsync("//Main");
    }

}
