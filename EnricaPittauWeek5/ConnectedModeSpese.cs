using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnricaPittauWeek5
{
    public static class ConnectedModeSpese //inserire spesa, aggiornare spesa
    {
        public static string ConnectionString { get; set; }

        public static void AddSpesa()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Spese VALUES(@data, @categoria, @descrizione, @utente, @importo, @approvato)";
                //Console.Clear();
                Console.WriteLine("-------------Inserire una nuova spesa-----------");

                Console.Write("Data: ");
                insertCommand.Parameters.AddWithValue("@data", DateTime.Now);
                DateTime data = DateTime.Parse(Console.ReadLine());


                Console.Write("Categoria: ");
                int categoria = int.Parse(Console.ReadLine());
                insertCommand.Parameters.AddWithValue("@Categoria", categoria);

                Console.Write("Descrizione: ");
                string descrizione = Console.ReadLine();
                insertCommand.Parameters.AddWithValue("@descrizione", descrizione);

                Console.Write("Utente: ");
                string utente = Console.ReadLine();
                insertCommand.Parameters.AddWithValue("@utente", utente);

                Console.Write("Importo: ");
                decimal importo = decimal.Parse(Console.ReadLine());
                insertCommand.Parameters.AddWithValue("@importo", importo);

                Console.Write("Approvato: ");
                bool approvato = bool.Parse(Console.ReadLine());
                insertCommand.Parameters.AddWithValue("@approvato", approvato);

                
                int result = insertCommand.ExecuteNonQuery();
                if (result != 1)
                    Console.WriteLine("Si è verificato un problema nell'inserimento della spesa");
                else
                    Console.WriteLine("Spesa aggiuta correttamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("-----Premi un tasto------ ");
                Console.ReadKey();
            }
        }
        public static void UpdateApprovazioneSpesa()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "UPDATE Spese SET Approvato = 1 WHERE Id=@Id";
                //Console.Clear();
                Console.WriteLine("-------------Approva spesa-----------");

                Console.Write("Iserisci Id della spesa da aggiornare: ");
                int idSpesa = int.Parse(Console.ReadLine());
                insertCommand.Parameters.AddWithValue("@Id", idSpesa);
                
                int result = insertCommand.ExecuteNonQuery();
                if (result != 1)
                    Console.WriteLine("Si è verificato un problema con l'aggiornamento della spesa");
                else
                    Console.WriteLine("Spesa aggiornata correttamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("------Premi un tasto------");
                Console.ReadKey();
            }
        }
        public static void VisualizzaSpeseApprovate()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = connection.CreateCommand();
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "select * from Spese where Approvato = 1";

                SqlDataReader reader = selectCommand.ExecuteReader();
                
                Console.WriteLine("------Elenco spese approvate------\n");
                
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var data = (DateTime)reader["Data"];
                    var descrizione = (string)reader["Descrizione"];
                    var utente = (string)reader["Utente"];
                    var importo = (decimal)reader["Importo"];
                    var approvato = (bool)reader["Approvato"];


                    Console.WriteLine($"{id} - {data} - {descrizione} - {utente} - {importo} - {approvato}");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("-----Premi un tasto------ ");
                Console.ReadKey();
            }
        }
        //public static void VisualizzaSpeseUtente()
        //{
        //    using SqlConnection connection = new SqlConnection(ConnectionString);
        //    try
        //    {
        //        connection.Open();
        //        Console.WriteLine("Inserisci Utente: ");
        //        string utente = Console.ReadLine();

        //        string query = "select * from Spese where Utente = utente";



        //        SqlCommand selectCommand = new SqlCommand();
        //        selectCommand.Connection = connection;       
        //        selectCommand.CommandType = System.Data.CommandType.Text;
        //        selectCommand.CommandText = query;

        //        SqlDataReader reader = selectCommand.ExecuteReader();
              

        //        Console.WriteLine("------Elenco spese dell'utente inserito------\n");
              
        //        {
        //            var id = (int)reader["Id"];
        //            var data = (DateTime)reader["Data"];
        //            var descrizione = (string)reader["Descrizione"];
        //            //var utente = (string)reader["Utente"];
        //            var importo = (decimal)reader["Importo"];
        //            var approvato = (bool)reader["Approvato"];


        //            Console.WriteLine($"{id} - {data.ToShortDateString()} - {descrizione} - {utente} - {approvato}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        Console.WriteLine("-----Premi un tasto------ ");
        //        Console.ReadKey();
        //    }
        //}

    }
}
