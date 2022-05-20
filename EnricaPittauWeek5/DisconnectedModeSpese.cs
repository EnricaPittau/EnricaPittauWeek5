using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnricaPittauWeek5
{
   public static class DisconnectedModeSpese //cancella spesa esistente
   {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpesa;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void DeleteSpesa()
        {
          DataSet dataset = new DataSet();
          using SqlConnection conn = new SqlConnection(connectionStringSQL);
          try
          {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Connessi al db");
            else
                Console.WriteLine("NON connessi al db");

            var spesaAdapter = InizializzaAdapter(conn);
            spesaAdapter.
                   Fill(dataset, "Spese");
               

            conn.Close();
            Console.WriteLine("Connessione chiusa");

            Console.WriteLine();
            Console.Write("Id spesa da cancellare: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Formato errato. Riprova. Id spesa da cancellare: ");
            };

            DataRow rigaDaEliminare = dataset.Tables["Spese"].Rows.Find(id);
            if (rigaDaEliminare != null)
            {
                rigaDaEliminare.Delete();
            }

            //riconciliazione e quindi vero salvataggio del dato sul db
            spesaAdapter.Update(dataset, "Spese");
            Console.WriteLine("Database aggiornato");
          }
          catch (SqlException ex)
          {
            Console.WriteLine($"Errore SQL: {ex.Message}");
          }
          catch (Exception ex)
          {
              Console.WriteLine($"Errore generico: {ex.Message}");
          }
          finally
          {
              conn.Close();
          }
        }

        private static SqlDataAdapter InizializzaAdapter(SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            //SELECT (serve al metodo FILL)
            adapter.SelectCommand = new SqlCommand("Select * from Spese", conn);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            //DELETE
            adapter.DeleteCommand = GeneraDeleteCommand(conn);

            return adapter;
        }

        private static SqlCommand GeneraDeleteCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Spese where Id=@id";

            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Id"));

            return cmd;
        }
   }
}
