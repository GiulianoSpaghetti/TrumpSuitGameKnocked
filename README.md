:it: Made in Italy. Il primo software in Maui che secondo Google non crasha.

Questo gioco dimostra che la teoria dei giochi è vera: l'algorimo brevettato funziona su tutti i giochi di carte senza piatto, questo in gergo è il "poker".

![Napoli-Logo](https://github.com/user-attachments/assets/8163c808-62d3-40d3-bce3-0957e57bc26a)
![made in parco grifeo](https://github.com/user-attachments/assets/fadbf046-aeae-4f11-bda4-eb332c701d56)

Best used on android 16.1



[fatti spiegare il progetto da gemini](https://g.co/gemini/share/a007ef5993e1) basta loggarsi col proprio account Google 

## TrumpSuitGameKnocked
Quello che avete davanti non è il gioco della briscola come si intende oggi, perché oggi tutti i simulatori di briscola dicono "hai preso l'asso, bravo" e finisce lì. Quello che avete davanti è un simulatore equo e professionale, con punteggio aggiornato in tempo reale, in modo da poter decidere se "rischiare" o meno coscientemente, scritto in maui, internazionalizzato in inglese, francese, spagnolo, tedesco, italiano e portoghese.
Su windows per cambiare il dialetto é sufficente modificare le impostazioni di sistema, su android 14 e 15 pure, sui precedenti bisogna disinstallare e reinstallare il programma.
Il gioco è più godibile sui tablet in modalità portrait, non landscape.
Sembra strano a dirsi, ma il gioco è hard core perché consente di cambiare in ogni istante l'andamento della partita cosicentemente con le proprie scelte.
L'assembly su cui si basa usa il linq.
Questa briscola si gioca fissandosi su un seme.

## Perché questo gioco è il meglio su android

Personalmente ho visto un po' in giro i nuovi giochini per android. Tutti molto semplici e banali, però sono delle macchinette mangiasoldi.

Sono gratuiti, però per passare la base dal livello 40 al livello 39 richiedono 500 euro: 300 per il legno, 200 per il fuoco e 6 giorni di tempo col cellulare acceso senza fare nulla. Io personalmente a queste condizioni preferisco NON GIOCARE, perché costa tutto, compresa la corrente e la batteria nuova del cellulare.

I giochi di carte anche sono gratuiti, però si accoppiano le carte per vincere. La briscola di nonno nanni non si capisce manco se è briscola, scopa o asso piglia tutto.

Il Trump Suit Game knocked sapete cosa promette? Per 5 euro LORDI ed UNA TANTUM promette un numero ben determinato e molto alto di partite a sessione, un gioco che segue ciecamente le regole della briscola classica (al meglio delle due partite), con un mazzo di carte semplice da capire ed una IA che cerca di prendere il maggior numero di carte, senza accoppiarsele, infatti il 3 può venire tranquillamente mangiato dall'asso ed AGGIORNAMENTI GRATUITI.

Vi sfido a fare di meglio.

## Lo use case

Ormai i giochi di carte sono vecchi e vengono fatti da pochissime persone. Vi immaginate il padre che compra al figlio di 3 anni un tablet android da 200 euro, con 5 euro la briscola e si mette a giocare aspettando il pullman o la metropolitana?

Nella mia briscola può capitare che il computer abbia il 3 e l'asso di briscola, facciamo denari, e che esca un carico, a quel punto da primo di mano gioca il carico.
Il bambino secondo di mano non ha né denari né come sopratagliare, non può prendere. Capita la prima volta, capita la seconda volta consecutiva, carica la terza volta consecutiva, a quel punto urla "Sto piombo a denari!".

E' questo il motto registrato, in entrambe le sue forme.

Negli anni 2000 capitava a casa grazie alla wxbriscola, oggi può capitare per la strada.

## Il gioco assistito

Con l'avvento delle IA google è riuscita a far capire alle stesse il funzionamento di base del gioco della briscola con risposta di seme, per cui su android con IA e sui computer windows con NPU è possibile farsi suggerire dalla IA la carta da giocare. Microsoft, con il winappsdk 1.8, si sta facendo uscire qualcosa di completamente innovativo, per cui spetta a voi decidere quale piattaforma usare, considerando che potete provarle tutte.

## Screenshots su windows

<img width="1920" height="2880" alt="Screenshot 2025-12-17 212032" src="https://github.com/user-attachments/assets/ab72088c-111a-41c2-941f-cdf0d338a506" />
<img width="1920" height="2880" alt="Screenshot 2025-12-17 212015" src="https://github.com/user-attachments/assets/69402cad-9717-47da-bb51-c1060a064c6e" />
<img width="1920" height="2880" alt="Screenshot 2025-12-17 211937" src="https://github.com/user-attachments/assets/970a7787-87bc-4304-a9b9-63076f9fc170" />
<img width="1920" height="2880" alt="Screenshot 2025-12-17 211916" src="https://github.com/user-attachments/assets/c6e04958-3141-4f65-b7e4-9b732c9e5ce5" />




## Video di presentazione per Windows

https://youtu.be/1APDQovhf2A?si=-MYioS_xQskDx4gs

## Video di presentazione per android

https://www.youtube.com/watch?v=NZ0fEwYRjh4

## Come installare

## Su Android

[![google](https://play.google.com/intl/it_it/badges/static/images/badges/en_badge_web_generic.png)](https://play.google.com/store/apps/details?id=org.altervista.numerone.trumpsuitgameknocked)

## Su Windows

Basta prendere l'msix che più piace dalle release su github, che sono controllate e non contengono virus. Il package msix è associato ad un certificato .cer che bisogna installare in "Computer locale" > "Persone Attendibili".

Prerequisiti: 

https://winstall.app/apps/Microsoft.DotNet.DesktopRuntime.10

Oppure 9 o o 8 a seconda di quello che scegliete.
E' consigliabile avere l'appruntime 1.8 installato sul computer (https://winstall.app/apps/Microsoft.WindowsAppRuntime.1.8), anche se si utilizza il desktop runtime 9 o 10. Tutto da provare...

## Aggiornamenti

Per windows i package msix sono platform indepedent ed in IL, ma sono in dotnet 9 e 10, per cui è necessario ricompilare per evitare di avere il sistema spurio in caso di nuovo dotnet framework che comunque è necessario per l'avvio del software, che se aggiornato dovrebbe impedire lo shock sulle ventole.

## Bug noti

Per windows serve il dotnet 9.0.4, perché è il primo su cui il linq funziona.

Se buttate il computer dalla finestra potrebbe non aprirsi più il software xD

Se il cellulare finisce sotto un tram risulta impossibile avviare il software xD


## L'ottimizzazione dello STATIC

Dal punto di vista tecnico, usare sempre le stesse variabili static per salvare i dati è già un'ottimizzazione ed è stato deciso di escludere le variabili static dal garbage collector per piegare questa ottimizzazione perché oramai l'ottimizzazione la fa il compilatore.

Quello che è successo è che cugino bruno e amica francesca (non la mia "francesca"), hanno scoperto che utilizzando le variabili static il cellulare andava in out of memory dopo appena 7 partite consecutive, e la mia francesca ha capito che l'ottimizzazione static è stata bannata a partire da dotnet 8.0.4.

Ora su android non va in out of memory, ma rallenta.


## Donazioni

http://numerone.altervista.org/donazioni.php
