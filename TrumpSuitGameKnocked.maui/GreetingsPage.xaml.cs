using org.altervista.numerone.framework;

namespace TrumpSuitGameKnocked.maui;

public partial class GreetingsPage : ContentPage
{
    private static Giocatore g, cpu;
    private static Mazzo m;
    private static UInt128 partite;
    private static UInt16 puntiUtente, puntiCpu;
	public GreetingsPage(Giocatore gi, Giocatore cp, GiocatoreHelperCpu helper, Mazzo mazzo, UInt16 vecchiPuntiUtente, UInt16 vecchiPuntiCpu, UInt128 NumeroPartite)
	{
		InitializeComponent();
        Title = $"{App.Dictionary["PartitaFinita"]}";
        String s, s1;
        g = gi;
        cpu= cp;
        m = mazzo;
        partite = NumeroPartite;
        puntiUtente = (UInt16) (vecchiPuntiUtente+g.Punteggio);
        puntiCpu = (UInt16) (vecchiPuntiCpu + cpu.Punteggio);
        if (puntiUtente == puntiCpu)
            s = $"{App.Dictionary["PartitaPatta"]}";
        else
        {
            if (puntiUtente > puntiCpu)
                s = $"{App.Dictionary["HaiVinto"]}";
            else
                s = $"{App.Dictionary["HaiPerso"]}";
            s = $"{s} {App.Dictionary["per"]} {Math.Abs(puntiUtente - puntiCpu)} {App.Dictionary["punti"]}";
        }
        if (NumeroPartite % 2 == 1)
            s1 = App.Dictionary["EffettuaNuovaPartita"] as string;
        else
        {
            s1 = App.Dictionary["EffettuaSecondaPartita"] as string;
        }
        fpRisultrato.Text = $"{App.Dictionary["PartitaFinita"]}. {s}. {s1}";
        btnNo.Text = $"{App.Dictionary["No"]}";
        btnShare.Text = $"{App.Dictionary["Condividi"]}";
        btnShare.IsEnabled = helper.GetLivello() == 3;
        btnShare.IsVisible = NumeroPartite%2==1;

    }


    private async void OnFPShare_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text={App.Dictionary["ColGioco"]}%20{partite+1}%20{g.Nome}%20{App.Dictionary["contro"]}%20{cpu.Nome}%20{App.Dictionary["efinito"]}%20{puntiUtente}%20{App.Dictionary["a"]}%20{puntiCpu}%20{App.Dictionary["piattaforma"]}%20{App.Piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGameKnocked.maui"));
        btnShare.IsEnabled = false;
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
