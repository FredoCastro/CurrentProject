using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImportLeagueWebAPI.Models
{
    [Table("Teams")]
    public class TeamModel : ITeamModel {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tla { get; set; }
        public string ShortName { get; set; }

        //Relacion 1-1 con Area
        public int? AreaId { get; set; }

        [ForeignKey("Id")]
        [Required]
        [NotMapped]
        public virtual AreaModel Area { get; set; }
        public string Email { get; set; }

        //Relacion 1-many con Competition
        public virtual CompetitionModel Competition { get; set; }

        List<PlayerModel> _squad;

        //Relacion 1-many con Player
        public List<PlayerModel> Squad {
            get { return _squad; }
            set {
                if (value == null)
                    throw new ArgumentNullException("squad");
                else
                    _squad = value;
            }
                 }
    }

    public class TeamRootObject {
        public ICollection<TeamModel> Teams { get; set; }
    }
}