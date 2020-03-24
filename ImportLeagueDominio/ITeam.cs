using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DatosEntidades;
using DTOEntidades;

namespace ImportLeagueDominio
{
    public interface ITeam
    {
        void Adicionar(ITeamModel entidad);
        void Eliminar(ITeamModel entidad);
        void Modificar(ITeamModel entidad);
        IEnumerable<ITeamModel> TraerTodo();
        ITeamModel TraerUno(Expression<Func<ITeamModel, bool>> predicado);
        ITeamModel TraerUnoPorId(int Id);

        List<TeamDTO> ImportTeamsFromCompetition(string _competitionId);
    }
}