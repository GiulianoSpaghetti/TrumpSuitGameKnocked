:it: Made in Italy. Il primo software in Maui che secondo Google non crasha.

Questo gioco dimostra che la teoria dei giochi è vera: l'algorimo brevettato funziona su tutti i giochi di carte senza piatto, questo in gergo è il "poker".

![Napoli-Logo](https://github.com/user-attachments/assets/8163c808-62d3-40d3-bce3-0957e57bc26a)
![made in parco grifeo](https://github.com/user-attachments/assets/fadbf046-aeae-4f11-bda4-eb332c701d56)


![made-with-microsoft-maui](https://github.com/user-attachments/assets/5a4451df-7baf-44e9-b73d-001bd59c6ecd)

[![forthebadge](https://forthebadge.com/images/badges/approved-by-my-mom.svg)](https://forthebadge.com)


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

## Screenshots su windows
![Immagine 2025-04-16 201707](https://github.com/user-attachments/assets/148a6231-2ac1-438a-97b2-b4df2be7609a)
![Immagine 2025-04-16 201638](https://github.com/user-attachments/assets/cac92010-b24d-45ac-9038-e6811acaa173)
![Immagine 2025-04-16 201428](https://github.com/user-attachments/assets/80cb4687-e64f-4c92-9459-1d4a9f62240a)



## Video di presentazione per Windows

https://photos.app.goo.gl/PUg7toWveeeDj1MF6

## Video di presentazione per android

https://youtu.be/coAx5hXJi5s

## Come installare

## Su Android

[![google](https://play.google.com/intl/it_it/badges/static/images/badges/en_badge_web_generic.png)](https://play.google.com/store/apps/details?id=org.altervista.numerone.trumpsuitgameknocked)

## Aggiornamenti

Per windows i package msix sono platform indepedent ed in IL, per cui è sufficiente scaricarsi il nuovo dotnet framework runtime e reinstallarsi il pacchetto per ottenere il codice binario ottimizzato con le ultime patch.

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
