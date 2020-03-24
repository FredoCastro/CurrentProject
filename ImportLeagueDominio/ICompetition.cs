using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DatosEntidades;
using DTOEntidades;
namespace ImportLeagueDominio
{
    public interface ICompetition
    {
        void Adicionar(ICompetitionModel entidad);
        void Eliminar(ICompetitionModel entidad);
        void Modificar(ICompetitionModel entidad);
        IEnumerable<ICompetitionModel> TraerTodo();
        ICompetitionModel TraerUno(Expression<Func<ICompetitionModel, bool>> predicado);
        ICompetitionModel TraerUnoPorId(int Id);
        CompetitionDTO ImportCompetition(string competitionId);
        
        //int SaveCompetition(CompetitionModel _competition);
        int SaveUniqueEntireCompetition(ICompetitionModel _competition, IPlayer playerLogic, ITeam teamLogic, IArea areaLogic );
    }
}