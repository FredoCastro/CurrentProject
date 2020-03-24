using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImportLeagueWebAPI.Models
{
    [Table("Players")]
    public class PlayerModel : IPlayerModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set;}
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }

        public int? ShirtNumber { get; set; }

        //Relacion 1-many con Team
        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public virtual TeamModel Team { get; set; }
    }
}