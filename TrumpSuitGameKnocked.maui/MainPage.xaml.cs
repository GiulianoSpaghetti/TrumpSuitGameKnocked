using org.altervista.numerone.framework;
using Snackbar = CommunityToolkit.Maui.Alerts.Snackbar;

namespace TrumpSuitGameKnocked.maui;

public partial class MainPage : ContentPage
{
    private static Giocatore g, cpu, primo, secondo, temp;
    private static Mazzo m;
    private static Carta c, c1, briscola;
    private static bool aggiornaNomi = false, primoUtente=true;
    private static UInt16 secondi = 5, vecchiPuntiUtente=0, vecchiPuntiCPU=0;
    private static UInt128 numeroPartite=0;
    private static bool avvisaTalloneFinito = true, briscolaDaPunti = false;
    private static IDispatcherTimer t;
    private static TapGestureRecognizer gesture;
    private static ElaboratoreCarteBriscola e;
    private static GiocatoreHelperCpu helper;
    public static MainPage main;
    public MainPage()
    {
        this.InitializeComponent();
        main = this;
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        secondi = (UInt16)Preferences.Get("secondi", 5);
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        Carta.Inizializza(40, new org.altervista.numerone.framework.briscola.CartaHelper(e.CartaBriscola),
App.d["bastoni"] as string, App.d["coppe"] as string, App.d["denari"] as string, App.d["spade"] as string
);
        g = new Giocatore(new GiocatoreHelperUtente(), Preferences.Get("nomeUtente", "numerone"), 3);
        switch (Preferences.Get("livello", 3))
        {
            case 1: helper = new GiocatoreHelperCpu0(e.CartaBriscola); break;
            case 2: helper = new GiocatoreHelperCpu1(e.CartaBriscola); break;
            default: helper = new GiocatoreHelperCpu2(e.CartaBriscola); break;
        }
        cpu = new Giocatore(helper, Preferences.Get("nomeCpu", "numerona"), 3);
        primo = g;
        secondo = cpu;
        briscola = Carta.GetCarta(e.CartaBriscola);
        gesture = new TapGestureRecognizer();
        gesture.Tapped += Image_Tapped;
        for (UInt16 i = 0; i < 3; i++)
        {
            g.AddCarta(m);
            cpu.AddCarta(m);

        }
        VisualizzaImmagine(g.GetID(0), 1, 0, true);
        VisualizzaImmagine(g.GetID(1), 1, 1, true);
        VisualizzaImmagine(g.GetID(2), 1, 2, true);

        NomeUtente.Text = g.Nome;
        NomeCpu.Text = cpu.Nome;
        PuntiCpu.Text = $"{App.d["PuntiDiPrefisso"]} {cpu.Nome} {App.d["PuntiDiSuffisso"]}: {cpu.Punteggio}";
        PuntiUtente.Text = $"{App.d["PuntiDiPrefisso"]} {g.Nome} {App.d["PuntiDiSuffisso"]}: {g.Punteggio}";
        NelMazzoRimangono.Text = $"{App.d["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.d["carte"]}"; 
        CartaBriscola.Text = $"{App.d["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
        Level.Text = $"{App.d["IlLivelloE"]}: {helper.GetLivello()}";
        Archivio.Text = $"{App.d["File"]}";
        Info.Text = $"{App.d["Info"]}";
        NuovaPartitaMenu.Text = $"{App.d["NuovaPartita"]}";
        OpzioniMenu.Text = $"{App.d["Opzioni"]}";
        EsciMenu.Text = $"{App.d["Esci"]}";
        InfoMenu.Text = $"{App.d["Informazioni"]}";
        VisualizzaImmagine(Carta.GetCarta(e.CartaBriscola).Id, 4, 4, false);

        t = Dispatcher.CreateTimer();
        t.Interval = TimeSpan.FromSeconds(secondi);
        t.Tick += (s, ex) =>
        {
            string snack = "";
            if (aggiornaNomi)
            {
                NomeUtente.Text = g.Nome;
                NomeCpu.Text = cpu.Nome;
                aggiornaNomi = false;
            }
            c = primo.CartaGiocata;
            c1 = secondo.CartaGiocata;
            ((Image)this.FindByName(c.Id)).IsVisible = false;
            ((Image)this.FindByName(c1.Id)).IsVisible = false;
            if ((c.CompareTo(c1) > 0 && c.StessoSeme(c1)) || (c1.StessoSeme(briscola) && !c.StessoSeme(briscola)))
            {
                temp = secondo;
                secondo = primo;
                primo = temp;
            }

            primo.AggiornaPunteggio(secondo);
            PuntiCpu.Text = $"{App.d["PuntiDiPrefisso"]} {cpu.Nome} {App.d["PuntiDiSuffisso"]}: {cpu.Punteggio}";
            PuntiUtente.Text = $"{App.d["PuntiDiPrefisso"]} {g.Nome} {App.d["PuntiDiSuffisso"]}: {g.Punteggio}";
            if (AggiungiCarte())
            {
                NelMazzoRimangono.Text = $"{App.d["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.d["carte"]}";
                CartaBriscola.Text = $"{App.d["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
		switch (m.GetNumeroCarte()) 
		{
			case 2: if (avvisaTalloneFinito)
                        		snack = $"{App.d["IlTalloneEFinito"]}\r\n";
                        break;
                        case 0: ((Image)this.FindByName(Carta.GetCarta(e.CartaBriscola).Id)).IsVisible = false;
                    		NelMazzoRimangono.IsVisible = false;
                	break;
                }
                for (UInt16 i = 0; i < g.NumeroCarte; i++)
                {
                    VisualizzaImmagine(g.GetID(i), 1, i, true);
                    ((Image)this.FindByName("Cpu" + i)).IsVisible = true;
                }
                switch (cpu.NumeroCarte)
                {
                    case 2: Cpu2.IsVisible = false; break;
                    case 1: Cpu1.IsVisible = false; break;
                }

                if (primo == cpu)
                {
                    GiocaCpu();
                    if (cpu.CartaGiocata.StessoSeme(briscola))
                        snack += $"{App.d["LaCpuHaGiocatoIl"]} {cpu.CartaGiocata.Valore + 1} {App.d["Briscola"]}\n";
                    else if (cpu.CartaGiocata.Punteggio > 0)
                        snack += $"{App.d["LaCpuHaGiocatoIl"]} {cpu.CartaGiocata.Valore + 1} {App.d["di"]} {cpu.CartaGiocata.SemeStr}\n";
                    if (snack != "")
                        Snackbar.Make(snack.Trim()).Show(App.cancellationTokenSource.Token);
                }

            }
            else
            {
                Navigation.PushAsync(new GreetingsPage(g, cpu, helper, m, vecchiPuntiUtente, vecchiPuntiCPU, numeroPartite));
                if (numeroPartite % 2 == 1)
                {
                    vecchiPuntiUtente = 0;
                    vecchiPuntiCPU = 0;
                }
                else
                {
                    vecchiPuntiUtente = g.Punteggio;
                    vecchiPuntiCPU = cpu.Punteggio;
                }
                if (numeroPartite == UInt128.MaxValue)
                {
                    Snackbar.Make("Non hai giocato abbastanza per oggi?").Show(App.cancellationTokenSource.Token);
                    Application.Current.Quit();
                }
                else
                {
                    numeroPartite++;
                    NuovaPartita();
                }
            }
            t.Stop();
        };
    }

    private void VisualizzaImmagine(String id, UInt16 i, UInt16 j, bool abilitaGesture)
    {
        Image img;
        img = (Image)this.FindByName(id);
        Applicazione.SetRow(img, i);
        Applicazione.SetColumn(img, j);
        img.IsVisible = true;
        if (abilitaGesture)
            img.GestureRecognizers.Add(gesture);
        else
            img.GestureRecognizers.Clear();

    }
    private void GiocaUtente(Image img)
    {
        UInt16 quale = 0;
        Image img1;
        for (UInt16 i = 1; i < g.NumeroCarte; i++)
        {
            img1 = (Image)this.FindByName(g.GetID(i));
            if (img.Id == img1.Id)
                quale = i;
        }
        if (primo == g)
            g.Gioca(quale);
        else
            g.Gioca(quale, primo, true);
        VisualizzaImmagine(g.GetID(quale), 2, 0, false);
    }

    private void NuovaPartita()
    {
        Image img;
        UInt16 level = (UInt16)Preferences.Get("livello", 3);
        if (level != helper.GetLivello()) {
            Snackbar.Make($"{App.d["NuovaPartitaPerLivello"]}").Show(App.cancellationTokenSource.Token);
            numeroPartite = 0;
        }
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        Carta.SetHelper(new org.altervista.numerone.framework.briscola.CartaHelper(e.CartaBriscola), m);
        briscola = Carta.GetCarta(e.CartaBriscola);
        g = new Giocatore(new GiocatoreHelperUtente(), g.Nome, 3);
        switch (level)
        {
            case 1: helper = new GiocatoreHelperCpu0(e.CartaBriscola); break;
            case 2: helper = new GiocatoreHelperCpu1(e.CartaBriscola); break;
            default: helper = new GiocatoreHelperCpu2(e.CartaBriscola); break;
        }
        cpu = new Giocatore(helper, cpu.Nome, 3);
        for (UInt16 i = 0; i < 40; i++)
        {
            img = (Image)this.FindByName(Carta.GetCarta(i).Id);
            img.IsVisible = false;
        }
        for (UInt16 i = 0; i < 3; i++)
        {
            g.AddCarta(m);
            cpu.AddCarta(m);
        }
        VisualizzaImmagine(g.GetID(0), 1, 0, true);
        VisualizzaImmagine(g.GetID(1), 1, 1, true);
        VisualizzaImmagine(g.GetID(2), 1, 2, true);

        Cpu0.IsVisible = true;
        Cpu1.IsVisible = true;
        Cpu2.IsVisible = true;

        PuntiCpu.Text = $"{App.d["PuntiDiPrefisso"]} {cpu.Nome}  {App.d["PuntiDiSuffisso"]}: {cpu.Punteggio}";
        PuntiUtente.Text = $"{App.d["PuntiDiPrefisso"]} {g.Nome} {App.d["PuntiDiSuffisso"]}: {g.Punteggio}";
        NelMazzoRimangono.Text = $"{App.d["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.d["carte"]}";
        CartaBriscola.Text = $"{App.d["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
        Level.Text = $"{App.d["IlLivelloE"]}: {helper.GetLivello()}";
        NelMazzoRimangono.IsVisible = true;
        CartaBriscola.IsVisible = true;
        primoUtente = !primoUtente;
        if (primoUtente)
        {
            primo = g;
            secondo = cpu;
        } else
        {
            primo = cpu;
            secondo = g;
            GiocaCpu();
        }
        VisualizzaImmagine(Carta.GetCarta(e.CartaBriscola).Id, 4, 4, false);
        Applicazione.IsVisible = true;
    }
    private void GiocaCpu()
    {
        UInt16 quale = 0;
        Image img1 = Cpu0;
        if (primo == cpu)
            cpu.Gioca(0);
        else
            cpu.Gioca(0, g, true);
        quale = cpu.ICartaGiocata;
        img1 = (Image)this.FindByName("Cpu" + quale);
        img1.IsVisible = false;
        VisualizzaImmagine(cpu.CartaGiocata.Id, 2, 2, false);
    }
    private bool AggiungiCarte()
    {
        try
        {
            primo.AddCarta(m);
            secondo.AddCarta(m);
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
        return true;
    }

    private void Image_Tapped(object Sender, EventArgs arg)
    {
        if (t.IsRunning)
            return;
        Image img = (Image)Sender;
        try
        {
            GiocaUtente(img);
        }
        catch (Exception ex)
        {
            Snackbar.Make(App.d["MossaNonConsentita"] as string).Show(App.cancellationTokenSource.Token);
            return;
        }
        t.Start();
        if (secondo == cpu)
            GiocaCpu();
    }

    public void AggiornaOpzioni()
    {
        UInt16 level = (UInt16)Preferences.Get("livello", 3);
        g.Nome=Preferences.Get("nomeUtente", "");
        cpu.Nome=Preferences.Get("nomeCpu", "");
        secondi = (UInt16)Preferences.Get("secondi", 5);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        t.Interval = TimeSpan.FromSeconds(secondi);
        aggiornaNomi = true;
        if (level != helper.GetLivello())
            NuovaPartita();
    }

    private void OnNuovaPartita_Click(object sender, EventArgs evt)
    {
        if (numeroPartite > UInt128.MaxValue - 2)
        {
            Snackbar.Make("Non hai giocato abbastanza per oggi?").Show(App.cancellationTokenSource.Token);
            Application.Current.Quit();
        }
        else
        {

            if (numeroPartite % 2 == 1)
                numeroPartite++;
            else
                numeroPartite += 2;
            vecchiPuntiUtente = 0;
            vecchiPuntiCPU = 0;
            NuovaPartita();
        }
    }
    private void OnInfo_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPage());
    }


    private void OnOpzioni_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OpzioniPage());
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
