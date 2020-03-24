using System;

namespace DatosEntidades
{
    public interface IPlayerModel {
        string CountryOfBirth { get; set; }
        DateTime? DateOfBirth { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Nationality { get; set; }
        int? ShirtNumber { get; set; }
        string Position { get; set; }
        int? TeamId { get; set; }
        TeamModel Team { get; set; }
    }
}