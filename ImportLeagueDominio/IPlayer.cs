using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DatosEntidades;
using DTOEntidades;

namespace ImportLeagueDominio
{
    public interface IPlayer
    {
        void Adicionar(IPlayerModel entidad);
        void Eliminar(IPlayerModel entidad);
        void Modificar(IPlayerModel entidad);
        IEnumerable<IPlayerModel> TraerTodo();
        IPlayerModel TraerUno(Expression<Func<IPlayerModel, bool>> predicado);
        IPlayerModel TraerUnoPorId(int Id);
        List<PlayerDTO> ImportPlayersFromTeam(int _teamId);
        int SavePlayerList(ICollection<PlayerModel> _player);
    }
}