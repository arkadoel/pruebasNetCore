using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace aa_consola
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("Elija una opcion:");
            menu.AppendLine("\t1.- Ejecucion normal");
            menu.AppendLine("\t2.- Conectar con BBDD");
            menu.AppendLine("\tQ.- Salir");
            menu.AppendLine("");
            bool seguir = true;

            do
            {
                System.Console.WriteLine(menu);
                System.Console.Write(">");
                string opcion = Console.ReadLine().ToUpper().Trim();

                switch (opcion)
                {
                    case "Q":
                        seguir = false;
                        break;
                    case "1":
                        programa1();
                        break;
                    case "2":
                        ConexionBBDD_SQLServer();
                        break;
                }
            } while (seguir);
        }


        private static void ConexionBBDD_SQLServer()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\REPOS\aaa_mio\IDI\MultipleTableReturn\MultipleTableReturn\Database1.mdf;Integrated Security=True");

            try
            {
                SqlCommand command = new SqlCommand("select * from Personas; select * from Cosas;", conn);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = command;

                conn.Open();
                da.Fill(ds);
                conn.Close();

                //para poder usar Field hay que tener la System.Data.DataSetExtensions
                List<Persona> personas = ds.Tables[0].Rows.Cast<DataRow>().Select(dataRow => new Persona()
                {
                    Id = dataRow.Field<int>("Id"),
                    Edad = dataRow.Field<int>("Edad"),
                    Nombre = dataRow.Field<string>("Nombre")
                }).ToList();

                foreach(Persona p in personas)
                {
                    Console.WriteLine("\t\t{0} - {1} {2}", p.Id, p.Nombre, p.Edad);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private static void programa1()
        {
            Console.WriteLine("Hola mundo!");
            System.Console.WriteLine("La hora actual es: {0}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());

            List<string> nombres = new List<string>(){
                "Fernando",
                "Luisa",
                "Paco"
            };
            foreach (string nombre in nombres)
            {
                System.Console.WriteLine("Nombre " + nombre);
            }

            Console.ReadLine();
        }

    }
}
