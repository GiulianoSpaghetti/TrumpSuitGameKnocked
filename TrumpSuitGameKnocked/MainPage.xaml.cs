using org.altervista.numerone.framework;
using System.Globalization;
using Snackbar = CommunityToolkit.Maui.Alerts.Snackbar;

namespace TrumpSuitGameKnocked;

public partial class MainPage : ContentPage
{
    private Giocatore g, cpu, primo, secondo, temp;
    private Mazzo m;
    private Carta c, c1, briscola;
    private bool aggiornaNomi = false, primoUtente=true;
    private UInt16 secondi = 5, vecchiPuntiUtente=0, vecchiPuntiCPU=0;
    private UInt128 numeroPartite=0;
    private bool avvisaTalloneFinito = true, briscolaDaPunti = false;
    private IDispatcherTimer t;
    private TapGestureRecognizer gesture;
    private ElaboratoreCarteBriscola e;
    private GiocatoreHelperCpu helper;
    private String mazzo;
    public static MainPage MainPageInstance { get; private set; }
    public MainPage()
    {
        this.InitializeComponent();
        MainPageInstance = this;
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        secondi = (UInt16)Preferences.Get("secondi", 5);
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        mazzo = Preferences.Get("mazzo", "Napoletano");
        m = new Mazzo(e, "Napoletano");
        if (mazzo != "Napoletano")
            CaricaMazzo(mazzo);
        Carta.Inizializza(m, 40, new org.altervista.numerone.framework.briscola.CartaHelper(e.CartaBriscola),
App.Dictionary["bastoni"] as string, App.Dictionary["coppe"] as string, App.Dictionary["denari"] as string, App.Dictionary["spade"] as string, App.Dictionary["Fiori"] as string, App.Dictionary["Quadri"] as string, App.Dictionary["Cuori"] as string, App.Dictionary["Picche"] as string
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
        VisualizzaImmagine(g.GetID(0), 3, 0, true);
        VisualizzaImmagine(g.GetID(1), 3, 1, true);
        VisualizzaImmagine(g.GetID(2), 3, 2, true);

        NomeUtente.Text = g.Nome;
        NomeCpu.Text = cpu.Nome;
        PuntiCpu.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {cpu.Nome} {App.Dictionary["PuntiDiSuffisso"]}: {cpu.Punteggio}";
        PuntiUtente.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {g.Nome} {App.Dictionary["PuntiDiSuffisso"]}: {g.Punteggio}";
        NelMazzoRimangono.Text = $"{App.Dictionary["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.Dictionary["carte"]}"; 
        CartaBriscola.Text = $"{App.Dictionary["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
        Level.Text = $"{App.Dictionary["IlLivelloE"]}: {helper.GetLivello()}";
        Archivio.Text = $"{App.Dictionary["File"]}";
        Info.Text = $"{App.Dictionary["Info"]}";
        NuovaPartitaMenu.Text = $"{App.Dictionary["NuovaPartita"]}";
        OpzioniMenu.Text = $"{App.Dictionary["Opzioni"]}";
        EsciMenu.Text = $"{App.Dictionary["Esci"]}";
        InfoMenu.Text = $"{App.Dictionary["Informazioni"]}";
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
            PuntiCpu.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {cpu.Nome} {App.Dictionary["PuntiDiSuffisso"]}: {cpu.Punteggio}";
            PuntiUtente.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {g.Nome} {App.Dictionary["PuntiDiSuffisso"]}: {g.Punteggio}";
            if (AggiungiCarte())
            {
                NelMazzoRimangono.Text = $"{App.Dictionary["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.Dictionary["carte"]}";
                CartaBriscola.Text = $"{App.Dictionary["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
		switch (m.GetNumeroCarte()) 
		{
			case 2: if (avvisaTalloneFinito)
                        		snack = $"{App.Dictionary["IlTalloneEFinito"]}\r\n";
                        break;
                        case 0: ((Image)this.FindByName(Carta.GetCarta(e.CartaBriscola).Id)).IsVisible = false;
                    		NelMazzoRimangono.IsVisible = false;
                	break;
                }
                for (UInt16 i = 0; i < g.NumeroCarte; i++)
                {
                    VisualizzaImmagine(g.GetID(i), 3, i, true);
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
                        snack += $"{App.Dictionary["LaCpuHaGiocatoIl"]} {cpu.CartaGiocata.Valore + 1} {App.Dictionary["Briscola"]}\n";
                    else if (cpu.CartaGiocata.Punteggio > 0)
                        snack += $"{App.Dictionary["LaCpuHaGiocatoIl"]} {cpu.CartaGiocata.Valore + 1} {App.Dictionary["di"]} {cpu.CartaGiocata.SemeStr}\n";
				}
                if (snack != "")
                    Snackbar.Make(snack.Trim()).Show(App.cancellationTokenSource.Token);

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
            Snackbar.Make($"{App.Dictionary["NuovaPartitaPerLivello"]}").Show(App.cancellationTokenSource.Token);
            numeroPartite = 0;
        }
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e, m.Nome);
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
        VisualizzaImmagine(g.GetID(0), 3, 0, true);
        VisualizzaImmagine(g.GetID(1), 3, 1, true);
        VisualizzaImmagine(g.GetID(2), 3, 2, true);

        Cpu0.IsVisible = true;
        Cpu1.IsVisible = true;
        Cpu2.IsVisible = true;

        PuntiCpu.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {cpu.Nome}  {App.Dictionary["PuntiDiSuffisso"]}: {cpu.Punteggio}";
        PuntiUtente.Text = $"{App.Dictionary["PuntiDiPrefisso"]} {g.Nome} {App.Dictionary["PuntiDiSuffisso"]}: {g.Punteggio}";
        NelMazzoRimangono.Text = $"{App.Dictionary["NelMazzoRimangono"]} {m.GetNumeroCarte()} {App.Dictionary["carte"]}";
        CartaBriscola.Text = $"{App.Dictionary["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
        Level.Text = $"{App.Dictionary["IlLivelloE"]}: {helper.GetLivello()}";
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
            cpu.Gioca(0, true);
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
            Snackbar.Make(App.Dictionary["MossaNonConsentita"] as string).Show(App.cancellationTokenSource.Token);
            return;
        }
        t.Start();
        if (secondo == cpu)
            GiocaCpu();
    }

    public void AggiornaOpzioni()
    {
        UInt16 level = (UInt16)Preferences.Get("livello", 3);
        string s;
        g.Nome=Preferences.Get("nomeUtente", "");
        cpu.Nome=Preferences.Get("nomeCpu", "");
        secondi = (UInt16)Preferences.Get("secondi", 5);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        t.Interval = TimeSpan.FromSeconds(secondi);
        s = Preferences.Get("mazzo", "Napoletano");
        if (s != mazzo)
        {
            CaricaMazzo(s); ;
            Carta.SetSemiStr(m,
App.Dictionary["bastoni"] as string, App.Dictionary["coppe"] as string, App.Dictionary["denari"] as string, App.Dictionary["spade"] as string, App.Dictionary["Fiori"] as string, App.Dictionary["Quadri"] as string, App.Dictionary["Cuori"] as string, App.Dictionary["Picche"] as string
);
            CartaBriscola.Text = $"{App.Dictionary["IlSemeDiBriscolaE"]}: {briscola.SemeStr}";
            mazzo = s;
        }
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
    private void LoadSicilianoDeck()
    {
        n0.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\0.png").Result);
        n1.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\1.png").Result);
        n2.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\2.png").Result);
        n3.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\3.png").Result);
        n4.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\4.png").Result);
        n5.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\5.png").Result);
        n6.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\6.png").Result);
        n7.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\7.png").Result);
        n8.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\8.png").Result);
        n9.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\9.png").Result);
        n10.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\10.png").Result);
        n11.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\11.png").Result);
        n12.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\12.png").Result);
        n13.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\13.png").Result);
        n14.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\14.png").Result);
        n15.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\15.png").Result);
        n16.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\16.png").Result);
        n17.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\17.png").Result);
        n18.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\18.png").Result);
        n19.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\19.png").Result);
        n20.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\20.png").Result);
        n21.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\21.png").Result);
        n22.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\22.png").Result);
        n23.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\23.png").Result);
        n24.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\24.png").Result);
        n25.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\25.png").Result);
        n26.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\26.png").Result);
        n27.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\27.png").Result);
        n28.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\28.png").Result);
        n29.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\29.png").Result);
        n30.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\30.png").Result);
        n31.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\31.png").Result);
        n32.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\32.png").Result);
        n33.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\33.png").Result);
        n34.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\34.png").Result);
        n35.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\35.png").Result);
        n36.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\36.png").Result);
        n37.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\37.png").Result);
        n38.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\38.png").Result);
        n39.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\39.png").Result);
        Cpu0.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\retro_carte_pc.png").Result);
        Cpu1.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\retro_carte_pc.png").Result);
        Cpu2.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\retro_carte_pc.png").Result);
        m.SetNome("Siciliano");
    }
    private void LoadNapoletanoDeck()
    {
        n0.Source = "n0.png";
        n1.Source = "n1.png";
        n2.Source = "n2.png";
        n3.Source = "n3.png";
        n4.Source = "n4.png";
        n5.Source = "n5.png";
        n6.Source = "n6.png";
        n7.Source = "n7.png";
        n8.Source = "n8.png";
        n9.Source = "n9.png";
        n10.Source = "n10.png";
        n11.Source = "n11.png";
        n12.Source = "n12.png";
        n13.Source = "n13.png";
        n14.Source = "n14.png";
        n15.Source = "n15.png";
        n16.Source = "n16.png";
        n17.Source = "n17.png";
        n18.Source = "n18.png";
        n19.Source = "n19.png";
        n20.Source = "n20.png";
        n21.Source = "n21.png";
        n22.Source = "n22.png";
        n23.Source = "n23.png";
        n24.Source = "n24.png";
        n25.Source = "n25.png";
        n26.Source = "n26.png";
        n27.Source = "n27.png";
        n28.Source = "n28.png";
        n29.Source = "n29.png";
        n30.Source = "n30.png";
        n31.Source = "n31.png";
        n32.Source = "n32.png";
        n33.Source = "n33.png";
        n34.Source = "n34.png";
        n35.Source = "n35.png";
        n36.Source = "n36.png";
        n37.Source = "n37.png";
        n38.Source = "n38.png";
        n39.Source = "n39.png";
        Cpu0.Source = "retro_carte_pc.png";
        Cpu1.Source = "retro_carte_pc.png";
        Cpu2.Source = "retro_carte_pc.png";
        m.SetNome("Napoletano");
    }
    private void LoadTrevigianoDeck()
    {
        n0.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\0.png").Result);
        n1.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\1.png").Result);
        n2.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\2.png").Result);
        n3.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\3.png").Result);
        n4.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\4.png").Result);
        n5.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\5.png").Result);
        n6.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\6.png").Result);
        n7.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\7.png").Result);
        n8.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\8.png").Result);
        n9.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\9.png").Result);
        n10.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\10.png").Result);
        n11.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\11.png").Result);
        n12.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\12.png").Result);
        n13.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\13.png").Result);
        n14.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\14.png").Result);
        n15.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\15.png").Result);
        n16.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\16.png").Result);
        n17.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\17.png").Result);
        n18.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\18.png").Result);
        n19.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\19.png").Result);
        n20.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\20.png").Result);
        n21.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\21.png").Result);
        n22.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\22.png").Result);
        n23.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\23.png").Result);
        n24.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\24.png").Result);
        n25.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\25.png").Result);
        n26.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\26.png").Result);
        n27.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\27.png").Result);
        n28.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\28.png").Result);
        n29.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\29.png").Result);
        n30.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\30.png").Result);
        n31.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\31.png").Result);
        n32.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\32.png").Result);
        n33.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\33.png").Result);
        n34.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\34.png").Result);
        n35.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\35.png").Result);
        n36.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\36.png").Result);
        n37.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\37.png").Result);
        n38.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\38.png").Result);
        n39.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\39.png").Result);
        Cpu0.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\retro_carte_pc.png").Result);
        Cpu1.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\retro_carte_pc.png").Result);
        Cpu2.Source = ImageSource.FromStream(() => FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\retro_carte_pc.png").Result);
        m.SetNome("Trevigiano");
    }

    private void CaricaMazzo(string quale)
    {
        switch (quale)
        {
            case "Siciliano":
                LoadSicilianoDeck();
                break;
            case "Trevigiano":
                LoadTrevigianoDeck();
                break;
            default:
                LoadNapoletanoDeck();
                break;
        }
    }
}

