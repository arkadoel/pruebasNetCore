using System;
using System.Collections.Generic;
using System.Linq;
using consolaYentityFramework.Models;

namespace consolaYentityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Metodos para crear conexion a bases de datos
                1.- Code-First: Crear la base de datos de cero (desde codigo) y manegar sus cambios de estructura
                             desde codigo con migraciones.
                2.- Database-first: Primero existe la base de datos y de ahi creamos todo el codigo y la conexion.

            Opcion: ");
            
            string opcion = Console.ReadLine();

            if(opcion == "1"){

            }
            else if (opcion == "2"){
                DatabaseFirstMethod();
            }

            Console.Write("\r\nEnter para terminar...");
            Console.ReadLine();
        }

        static void DatabaseFirstMethod()
        {
            using(var db = new Models.dbpruebasContext())
            {
                List<Persona> listaInsertar = new List<Persona>();
                
                for(int i=0; i<9; i++)
                {
                    Persona persona = new Persona(){
                        Name = "fer" + i.ToString(),
                        Description = "ninguna"
                    };

                    listaInsertar.Add(persona);
                }

                db.Persona.AddRange(listaInsertar.AsEnumerable());
                db.SaveChanges(true);

                System.Console.WriteLine("Se ha guardado la primera persona");

                System.Console.WriteLine("Listado de personas guardadas: ");
                var lista = db.Persona.Where(x=> x.Name.Contains("fer")).Distinct().ToList<Persona>();

                foreach(var personaListada in lista){
                    System.Console.WriteLine("\t{0}", personaListada.Name);
                }

                //eliminar los que tengan un 3
                var listaEliminar = db.Persona.Where(x=> x.Name.Contains("3"));

                db.Persona.RemoveRange(listaEliminar);
                db.SaveChanges(true);




            }//end using
        }
    }
}
