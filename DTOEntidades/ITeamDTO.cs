using System.Collections.Generic;

namespace DTOEntidades
{
    public interface ITeamDTO {
        AreaDTO Area { get; set; }
        CompetitionDTO Competition { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        List<PlayerDTO> Squad { get; set; }
        string Tla { get; set; }
    }
}