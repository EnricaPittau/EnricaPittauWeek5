// See https://aka.ms/new-console-template for more information
using EnricaPittauWeek5;

Console.WriteLine("-----------------Gestione spesa!-----------------");

string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpesa;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
ConnectedModeSpese.ConnectionString = connectionStringSQL;
//DisconnectedModeSpese.ConnectionString2 = connectionStringSQL;

bool quit = false;
do
{  
    Console.WriteLine("[1] - Inserire nuova spesa\n");
    Console.WriteLine("[2] - Approvare una spesa esistente\n"); 
    Console.WriteLine("[3] - Cancellare una spesa esistente\n"); 
    Console.WriteLine("[4] - Elenco spese approvate\n");
    Console.WriteLine("[5] - Elenco spese di uno specifico utente\n");
    Console.WriteLine("[6] - Totale spese per categoria\n");
    Console.WriteLine("[q] - QUIT\n");

    string scelta = Console.ReadLine();
    switch (scelta)
    {
        case "1":
            //Inserire nuova spesa
            ConnectedModeSpese.AddSpesa();
            break;
        case "2":
            //Approvare una spesa esistente
            ConnectedModeSpese.UpdateApprovazioneSpesa();
            break;
        case "3":
            //Cancellare una spesa esistente
            DisconnectedModeSpese.DeleteSpesa();
            break;
        case "4":
            //Elenco spese approvate
            ConnectedModeSpese.VisualizzaSpeseApprovate();
            break;
        case "5":
            //Elenco spese di uno specifico utente
            //ConnectedModeSpese.VisualizzaSpeseUtente();
            break;
        case "6":
            //Totale spese per categoria
            
            break;
        case "q":
            quit = true;
            break;
        default:
            Console.WriteLine("Comando sconosciuto");
            break;

    }
} while (!quit);