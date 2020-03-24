namespace ImportLeagueWebAPI.Models {
    public interface IAreaModel {
        CompetitionModel Competition { get; set; }
        int Id { get; set; }
        string Name { get; set; }
    }
}