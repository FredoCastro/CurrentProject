using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DatosEntidades;

namespace DatosRepositorio
{
    public class ImportLeagueContex : DbContext, IImportLeagueContex {

        //Propiedades
        public DbSet<AreaModel> Areas { get; set; }
        public DbSet<CompetitionModel> Competitions { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<PlayerModel> Players { get; set; }

        //Constructor con parametro cadena de conexion  desde webconfig
        public ImportLeagueContex():base("name=ImportLeagueDBConnectionString"){
            //Inicializador para desarrollo
            //Database.SetInitializer<ImportLeagueContex>(new DropCreateDatabaseIfModelChanges<ImportLeagueContex>());
            //Inicializador para produccion
            //Database.SetInitializer<ImportLeagueContex>(new CreateDatabaseIfNotExists<ImportLeagueContex>());
        }



    }
}