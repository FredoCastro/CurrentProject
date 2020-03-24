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
using DatosEntidades;
using Util = DatosAcceso;

namespace ImportLeagueDominio
{
    //Clase de negocio, posee un repositorio de su entidad
    public class Competition : ICompetition
    {
        //
        private IRepositorio<entity.ICompetitionModel> _repositorio;

        //Declaramos el contexto en el repositorio para que direct injection lo agregue 
        public Competition(IRepositorio<entity.ICompetitionModel> iImportLeagueContex)
        {
            _repositorio = iImportLeagueContex;
        }

        public void Adicionar(entity.ICompetitionModel entidad)
        {
            _repositorio.Adicionar(entidad);
            _repositorio.Grabar();
        }

        public void Modificar(entity.ICompetitionModel entidad)
        {
            _repositorio.Modificar(entidad);
            _repositorio.Grabar();
        }

        public void Eliminar(entity.ICompetitionModel entidad)
        {
            _repositorio.Eliminar(entidad);
            _repositorio.Grabar();
        }

        public IEnumerable<entity.ICompetitionModel> TraerTodo()
        {
            return _repositorio.TraerTodo().ToList();
        }

        public entity.ICompetitionModel TraerUnoPorId(int Id)
        {
            return _repositorio.TraerUnoPorId(Id);
        }

        public entity.ICompetitionModel TraerUno(Expression<Func<entity.ICompetitionModel, bool>> predicado)
        {
            return _repositorio.TraerUno(predicado);
        }

        public CompetitionDTO ImportCompetition(string competitionId)
        {

            CompetitionDTO _competition = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Util.Util.URICompetition());
                    client.DefaultRequestHeaders.Add(Util.Util.APITokenName(), Util.Util.APITokenValue());
                    //HTTP GET
                    var responseTask = client.GetAsync(competitionId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CompetitionDTO>();
                        readTask.Wait();
                        _competition = readTask.Result;
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

            return _competition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_competition"></param>
        /// <returns></returns>
        public int SaveUniqueEntireCompetition(ICompetitionModel _competition, IPlayer playerLogic, ITeam teamLogic, IArea areaLogic)
        {
            try
            {
                if (_repositorio != null)
                {
                    if (_competition.Id != 0 && _repositorio.TraerUnoPorId(_competition.Id) == null) {

                        //GUardamos el area de la competition

                        if (areaLogic.TraerUnoPorId(_competition.AreaId.Value) == null) {
                            areaLogic.Adicionar(_competition.Area);
                        }

                        if (_repositorio.TraerUnoPorId(_competition.Id) == null)
                        {
                            _repositorio.Adicionar(_competition);
                        }
                        else {
                            _repositorio.Modificar(_competition);
                        }

                            #region Team/Squad

                            foreach (TeamModel _team in _competition.Teams)
                            {
                            //Guardamos el Entity Model

                            if (teamLogic.TraerUnoPorId(_team.Id) == null)
                            {
                                teamLogic.Adicionar(_team);
                            }
                            else {
                                teamLogic.Modificar(_team);
                            }

                            if (areaLogic.TraerUnoPorId(_team.AreaId.Value) == null)
                            {
                                areaLogic.Adicionar(_team.Area);
                            }
                            else {
                                areaLogic.Modificar(_team.Area);
                            }
                                playerLogic.SavePlayerList(_team.Squad);
                            }

                        #endregion Team/Squad
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

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
