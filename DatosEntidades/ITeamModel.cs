using System.Collections.Generic;

namespace DatosEntidades
{
    public interface ITeamModel {
        AreaModel Area { get; set; }
        //string AreaName { get; set; }
        CompetitionModel Competition { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        List<PlayerModel> Squad { get; set; }
        string Tla { get; set; }
    }
}