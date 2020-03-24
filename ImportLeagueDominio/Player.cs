using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = DatosEntidades;
using DatosRepositorio;
using System.Linq.Expressions;
using DTOEntidades;
using System.Net.Http;
using DatosEntidades;
using Util = DatosAcceso;

namespace ImportLeagueDominio
{
    public class Player : IPlayer
    {
        //
        private IRepositorio<entity.IPlayerModel> _repositorio;

        //Declaramos el contexto en el repositorio para que direct injection lo agregue 
        public Player(IRepositorio<entity.IPlayerModel> iImportLeagueContex)
        {
            _repositorio = iImportLeagueContex;
        }

        public void Adicionar(entity.IPlayerModel entidad)
        {
            _repositorio.Adicionar(entidad);
            _repositorio.Grabar();
        }

        public void Modificar(entity.IPlayerModel entidad)
        {
            _repositorio.Modificar(entidad);
            _repositorio.Grabar();
        }

        public void Eliminar(entity.IPlayerModel entidad)
        {
            _repositorio.Eliminar(entidad);
            _repositorio.Grabar();
        }

        public IEnumerable<entity.IPlayerModel> TraerTodo()
        {
            return _repositorio.TraerTodo().ToList();
        }

        public entity.IPlayerModel TraerUnoPorId(int Id)
        {
            return _repositorio.TraerUnoPorId(Id);
        }

        public entity.IPlayerModel TraerUno(Expression<Func<entity.IPlayerModel, bool>> predicado)
        {
            return _repositorio.TraerUno(predicado);
        }
        public List<PlayerDTO> ImportPlayersFromTeam(int teamId)
        {

            List<PlayerDTO> _players = null;
            TeamDTO _rootTeams = null;

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Util.Util.URIPlayer());
                    client.DefaultRequestHeaders.Add(Util.Util.APITokenName(), Util.Util.APITokenValue());
                    //HTTP GET
                    var responseTask = client.GetAsync(teamId.ToString()); //{id}/teams
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<TeamDTO>();
                        readTask.Wait();
                        _rootTeams = readTask.Result;
                        _players = _rootTeams.Squad;
                    }
                    else
                    {
                        throw new Exception(result.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _players;
        }

        public int SavePlayerList(ICollection<PlayerModel> _players)
        {
            try
            {
                if (_repositorio != null && _players != null && _players.Count > 0)
                {
                    foreach (PlayerModel playerEntity in _players)
                    {
                        if (_repositorio.TraerUnoPorId(playerEntity.Id) == null)
                        {
                            _repositorio.Adicionar(playerEntity);
                        }
                        else {
                            _repositorio.Modificar(playerEntity);
                        }
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
