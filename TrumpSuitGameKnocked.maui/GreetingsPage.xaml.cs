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
        Title = $"{App.d["PartitaFinita"]}";
        String s, s1;
        g = gi;
        cpu= cp;
        m = mazzo;
        partite = NumeroPartite;
        puntiUtente = (UInt16) (vecchiPuntiUtente+g.Punteggio);
        puntiCpu = (UInt16) (vecchiPuntiCpu + cpu.Punteggio);
        if (puntiUtente == puntiCpu)
            s = $"{App.d["PartitaPatta"]}";
        else
        {
            if (puntiUtente > puntiCpu)
                s = $"{App.d["HaiVinto"]}";
            else
                s = $"{App.d["HaiPerso"]}";
            s = $"{s} {App.d["per"]} {Math.Abs(puntiUtente - puntiCpu)} {App.d["punti"]}";
        }
        if (NumeroPartite % 2 == 1)
            s1 = App.d["EffettuaNuovaPartita"] as string;
        else
        {
            s1 = App.d["EffettuaSecondaPartita"] as string;
        }
        fpRisultrato.Text = $"{App.d["PartitaFinita"]}. {s}. {s1}";
        btnNo.Text = $"{App.d["No"]}";
        btnShare.Text = $"{App.d["Condividi"]}";
        btnShare.IsEnabled = helper.GetLivello() == 3;
        btnShare.IsVisible = NumeroPartite%2==1;

    }


    private async void OnFPShare_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text={App.d["ColGioco"]}%20{partite+1}%20{g.Nome}%20{App.d["contro"]}%20{cpu.Nome}%20{App.d["efinito"]}%20{puntiUtente}%20{App.d["a"]}%20{puntiCpu}%20{App.d["piattaforma"]}%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGameKnocked.maui"));
        btnShare.IsEnabled = false;
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
