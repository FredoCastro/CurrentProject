namespace DTOEntidades
{
    public interface IAreaDTO {
        CompetitionDTO Competition { get; set; }
        int? Id { get; set; }
        string Name { get; set; }
    }
}