# Tutorial de Database First con Entity Framework y .net core 2

Creamos el proyecto de consola:
``` shell
dotnet new console -o consolaYentityFramework
```

Creamos la base de datos (en este caso postgreSQL)

``` sql
CREATE TABLE "persona"
(
    id serial NOT NULL,
    name varchar(120) NOT NULL,
    description varchar(50),
    PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);
ALTER TABLE "persona"
    OWNER to postgres;

CREATE TABLE "telefono"
(
    idTelefono serial NOT NULL,
    idPersona serial not null,
    phoneNumber varchar(11),
    PRIMARY KEY (idTelefono)
)
WITH (
    OIDS = FALSE
);
ALTER TABLE "telefono"
    OWNER to postgres;
```

Editamos el archivo .csproj y la parte de ItemGroup debe quedar de la siguiente manera

``` xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />   
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.0" />
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
    <DotNetCliToolReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="2.0.0" />

    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="2.0.0" />
  </ItemGroup>
</Project>
```
Con eso hemos referenciado las librerias necesarias para entity framework y postgresql. Haremos un *dotnet restore* que seguramente nos pida. 

Ahora (en el directorio donde tengamos el archivo *.csproj) ejecutamos lo siguiente para que nos genere las clases a la base de datos.
``` shell
dotnet ef dbcontext scaffold "Host=localhost;Database=dbpruebas;Username=postgres;Password=toor" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
``` 
con la opcion **-o** estaremos diciendo en qué carpeta queremos que lo genere.
(Si os sale 'No executable found command "dotnet-ef" --> comprobad que en el .csproj teneis tanto DotNetCliToolReference como PackageRererence de las librerias, en linux puede dar error)

Si ya tenemos unos modelos generados y queremos actualizarlos usaremos el parametro -f 
``` shell
dotnet ef dbcontext scaffold "Host=localhost;Database=dbpruebas;Username=postgres;Password=toor" Npgsql.EntityFrameworkCore.PostgreSQL -f -o Models
``` 
Un codigo sencillo para usarlo es este
``` csharp
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
```

## Recordatorio de instalacion Postgres
``` shell
sudo apt-get install postgresql postgresql-contrib
sudo passwd postgres
su - postgres
psql -d template1 -c "ALTER USER postgres WITH PASSWORD 'newpassword';"

createdb dbpruebas
``` 


