using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DatosEntidades;

namespace DatosRepositorio
{
    public interface IImportLeagueContex {
        DbSet<AreaModel> Areas { get; set; }
        DbSet<CompetitionModel> Competitions { get; set; }
        DbSet<TeamModel> Teams { get; set; }
        DbSet<PlayerModel> Players { get; set; }
        int SaveChanges();
    }
}