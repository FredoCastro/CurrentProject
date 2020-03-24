using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImportLeagueWebAPI.Models
{
    [Table("Competitions")]
    public class CompetitionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public string AreaName { get { return Area.Name; } }

        //Relacion 1-1 con Area
        
        public int? AreaId { get; set; }

        [ForeignKey("Id")]
        [Required]
        public virtual AreaModel Area {get; set;}
        
        //Relacion 1-many con Teams
        public virtual List<TeamModel> Teams { get; set; }

    }

}