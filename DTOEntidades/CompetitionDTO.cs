using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DTOEntidades
{
    public class CompetitionDTO : ICompetitionDTO {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public string AreaName { get {
                if (Area != null)
                    return Area.Name;
                else
                    return string.Empty;
            }
        }

        //Relacion 1-1 con Area
        public virtual AreaDTO Area {get; set;}

        //Relacion 1-many con Teams
        public virtual List<TeamDTO> Teams { get; set; }

    }

}