using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImportLeagueWebAPI.Models
{
    [Table("Areas")]
    public class AreaModel : IAreaModel {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relacion 1-1 con Competition
        public virtual CompetitionModel Competition {get; set;}

    }
}