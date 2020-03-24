using System;

namespace DTOEntidades
{
    public interface IPlayerDTO {
        string CountryOfBirth { get; set; }
        DateTime? DateOfBirth { get; set; }
        int? Id { get; set; }
        string Name { get; set; }
        string Nationality { get; set; }
        string Position { get; set; }
        TeamDTO Team { get; set; }
    }
}