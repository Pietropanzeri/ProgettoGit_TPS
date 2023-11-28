# Gnam Gnam!
![SQL](https://img.shields.io/badge/SQL-800000.svg?style=for-the-badge&logo=sqlite&logoColor=white)
[![C#](https://img.shields.io/badge/C%23-FFA500.svg?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Core 7.0](https://img.shields.io/badge/.NET_Core-FF1493.svg?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/3.1)
[![Microsoft.EntityFrameworkCore](https://img.shields.io/badge/Microsoft.EntityFrameworkCore-7FFF00.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Design](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Design-DC143C.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Sqlite](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Sqlite-4682B4.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Tools](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Tools-9400D3.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![UML](https://img.shields.io/badge/UML-000080.svg?style=for-the-badge)](https://en.wikipedia.org/wiki/Unified_Modeling_Language)
[![PlantUML](https://img.shields.io/badge/PlantUML-800000.svg?style=for-the-badge&logo=sqlite&logoColor=white)](https://plantuml.com/)
[![UML Class Diagram](https://img.shields.io/badge/UML_Class_Diagram-008080.svg?style=for-the-badge)](https://plantuml.com/class-diagram)
---

L'obbiettivo principale di questo progetto è quello di imparare nel migliore dei modi l'utilizzo di GIT sperimentandolo sul campo.

L'applicazione descritta è un social network dedicato alle ricette culinarie, progettato per connettere gli appassionati di cucina e gli amanti del cibo. Gli utenti possono sfruttare diverse funzionalità:
* **Creazione e Condivisione di Ricette:** gli utenti hanno la possibilità di creare e condividere le proprie ricette. Possono includere dettagli come ingredienti, istruzioni e eventuali immagini del piatto finito. Questo favorisce la condivisione di esperienze culinarie personali;
* **Ricerca Avanzata:** L'app offre una funzionalità di ricerca avanzata che consente agli utenti di cercare ricette specifiche o esplorare categorie culinarie. Gli utenti possono trovare ispirazione per nuovi piatti o cercare ricette specifiche basate su ingredienti, tempi di preparazione, o diete particolari;
* **Connessione con Amici:** Gli utenti possono connettersi con amici o altri appassionati di cucina, seguire i loro profili e ricevere aggiornamenti sulle nuove ricette che condividono. La possibilità di commentare e mettere "mi piace" favorisce l'interazione e la condivisione di suggerimenti;
* **Feed Personalizzato:** l'app crea un feed personalizzato per ciascun utente, mostrando le ricette e le attività dei loro amici e delle persone che seguono. Questo contribuisce a mantenere gli utenti coinvolti e ispirati nella loro avventura culinaria;

## Guida all'avvio del ricettario
Benvenuto alla guida per l'installazione del tuo ricettario preferito! Per poter fare funzionare nel migliore dei modi l'applicazione è necessario seguire alcuni semplici passi:
1. Avviare un DBMS mySql (vers. 8.0.30) sulla porta 3306 del proprio computer;
2. Creare un database di nome '*ricettariodb*' con i seguenti comandi da linea di comando:
- accesso al DBMS:
```bash
mysql -u root -p
```
- creazione database: 
```bash
create database ricettariodb;
```
3. Installazione delle librerie riportate all'inizio di questo documento in Visual Studio. Per farlo si può cliccare col tasto destro del mouse sul nome della soluzione e selezionare `Gestione pacchetti NuGet`. All'interno della finestra c'è la possibilità di digitare il nome delle librerie ed installarle;
4. Aprire la console di gestione pachhetti di Visual Studio e digitare i seguenti comandi:
```bash
Add-migration init
update-database 
```
5. Una volta completate tutte le operazioni si può avviare il server, il quale mostrerà nella console il suo indirizzo ip. 
Questo indirizzo va sostituito nel file *App.xaml.cs* del client al posto di *BaseHttp* e *BaseHttps* lasciando il numero della porta invariato.
6. Dopo aver cambiato tutto ciò si può avviare il client e sfruttare tutte le funzionalità disponibili nella nostra applicazione.


## Authors
Si ringraziano tutti coloro che hanno partecipato alla realizzazione del progetto. Nel caso in cui abbiate domande o problemi durante l'avvio dell'applicazione non esitate a contattarci ai seguenti indirizzi di posta elettronica:
- [@AndreaErtola](https://github.com/AndreaErtola) - (email: andrea.ertola@issgreppi.it)
- [@DanieleGalimberti](https://github.com/DanieleGalimberti)  - (email: daniele.galimberti@issgreppi.it)
- [@MarcoGhisoni](https://github.com/MarcoGhisoni) - (email: marco.ghisoni@issgreppi.it)
- [@DanieleLocatelli](https://github.com/LocatelliDaniele) - (email: daniele.locatelli@issgreppi.it)
- [@PietroPanzeri](https://github.com/Pietropanzeri) - (email: pietro.panzeri@issgreppi.it)



## License

Questo progetto è rilasciato sotto i termini della [ GNU General Public License v3.0](https://opensource.org/license/gpl-3-0/), una licenza che conferisce l'ampia libertà di utilizzare, modificare e distribuire il codice sorgente in accordo con le disposizioni specifiche in questa licenza.

È notevole importanza sottolineare che, in conformità con la licenza GNU GPL v3.0, qualsiasi modifica apportata a questo software e successivamente distribuita deve essere anch'essa pubblicata con la stessa licenza open source. Ciò garantisce la preservazione della natura aperta e condivisibile del codice, promuovendo la collaborazione e la condivisione all'interno della comunità degli sviluppatori.

Inoltre, è incoraggiato l'apporto di miglioramenti al progetto e la condivisione delle modifiche apportate con la comunità degli sviluppatori.
