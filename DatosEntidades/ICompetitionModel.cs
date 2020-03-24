using System.Collections.Generic;

namespace DatosEntidades
{
    public interface ICompetitionModel
    {
        AreaModel Area { get; set; }
        int? AreaId { get; set; }
        string AreaName { get; }
        string Code { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        List<TeamModel> Teams { get; set; }
    }
}