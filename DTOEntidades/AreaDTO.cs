using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTOEntidades

{
    public class AreaDTO : IAreaDTO {
        public int? Id { get; set; }
        public string Name { get; set; }

        //Relacion 1-1 con Competition
        public virtual CompetitionDTO Competition {get; set;}

    }
}