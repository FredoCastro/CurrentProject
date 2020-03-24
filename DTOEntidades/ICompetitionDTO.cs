using System.Collections.Generic;

namespace DTOEntidades
{
    public interface ICompetitionDTO {
        AreaDTO Area { get; set; }
        //string AreaName { get; }
        string Code { get; set; }
        int? Id { get; set; }
        string Name { get; set; }
        List<TeamDTO> Teams { get; set; }
    }
}