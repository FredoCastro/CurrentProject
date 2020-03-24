using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.ComponentModel.DataAnnotations.Schema;

namespace DTOEntidades
{
    public class TeamDTO : ITeamDTO {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tla { get; set; }
        public string ShortName { get; set; }

        //Relacion 1-1 con Area
        public virtual AreaDTO Area { get; set; }
        public string Email { get; set; }

        //Relacion 1-many con Competition
        public virtual CompetitionDTO Competition { get; set; }


        List<PlayerDTO> _squad;

        //Relacion 1-many con Player
        public List<PlayerDTO> Squad {
            get { return _squad; }
            set { _squad = value; }
            //set {
            //    if (value == null)
            //        throw new ArgumentNullException("squad");
            //}
                 }

        //[NotMapped]
        //public List<PlayerModel> squad { get; set; }
    }

    public class TeamRootObject {
        //public int count { get; set; }
        //public Filters filters { get; set; }
        //public Competition competition { get; set; }
        //public Season season { get; set; }
        public List<TeamDTO> Teams { get; set; }
    }
}