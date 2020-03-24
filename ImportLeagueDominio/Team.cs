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
using System.Web;
using Util = DatosAcceso;

namespace ImportLeagueDominio
{
    public class Team : ITeam
    {
        //
        private IRepositorio<entity.ITeamModel> _repositorio;

        //Declaramos el contexto en el repositorio para que direct injection lo agregue 
        public Team(IRepositorio<entity.ITeamModel> iImportLeagueContex)
        {
            _repositorio = iImportLeagueContex;
        }

        public void Adicionar(entity.ITeamModel entidad)
        {
            _repositorio.Adicionar(entidad);
            _repositorio.Grabar();
        }

        public void Modificar(entity.ITeamModel entidad)
        {
            _repositorio.Modificar(entidad);
            _repositorio.Grabar();
        }

        public void Eliminar(entity.ITeamModel entidad)
        {
            _repositorio.Eliminar(entidad);
            _repositorio.Grabar();
        }

        public IEnumerable<entity.ITeamModel> TraerTodo()
        {
            return _repositorio.TraerTodo().ToList();
        }

        public entity.ITeamModel TraerUnoPorId(int Id)
        {
            return _repositorio.TraerUnoPorId(Id);
        }

        public entity.ITeamModel TraerUno(Expression<Func<entity.ITeamModel, bool>> predicado)
        {
            return _repositorio.TraerUno(predicado);
        }
        public List<TeamDTO> ImportTeamsFromCompetition(string competitionId)
        {

            List<TeamDTO> _teams = null;

            TeamRootObject _rootTeams = null;

            try
            {

                //HttpDataAccess _dataAcces = new HttpDataAccess();

                //_rootTeams = _dataAcces.Import<TeamRootObject>(competitionId);

                //_teams = _rootTeams.Teams;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Util.Util.URICTeam());
                    client.DefaultRequestHeaders.Add(Util.Util.APITokenName(), Util.Util.APITokenValue());
                    //HTTP GET
                    var responseTask = client.GetAsync(competitionId + "/teams");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<TeamRootObject>();
                        readTask.Wait();
                        _rootTeams = readTask.Result;
                        _teams = _rootTeams.Teams;
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
            return _teams;
        }
    }
}
