using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using DTOEntidades;
using Dom = ImportLeagueDominio;
using DatosEntidades;
using DirectInjection;
using Autofac;
using System.Web.Mvc;

namespace ImportLeagueWebAPI.Controllers
{
    public class ImportController : ApiController
    {

        private IMapper _mapper;
        private IContainer container;

        public ImportController(IMapper mapper, IContainer container)
        {
            _mapper = mapper;

            this.container = container;
        }

        public ImportController()
        {
        }

        public HttpResponseMessage Get(string id)
        {

            CompetitionDTO _competitionDTO = null;

            try
            {

                //
                //var container = ContainerConfig.Configure();

                
                using (var scope = this.Configuration.DependencyResolver.BeginScope()) {

                    var _competition = scope.GetService(typeof(Dom.ICompetition));
                }

                using (var scope = container.BeginLifetimeScope())
                {

                    var _competitionLogic = scope.Resolve<Dom.ICompetition>();
                    var _teamLogic = scope.Resolve<Dom.ITeam>();
                    var _playerLogic = scope.Resolve<Dom.IPlayer>();
                    var _areaLogic = scope.Resolve<Dom.IArea>();

                    #region importando Conpeticion

                    _competitionDTO = _competitionLogic.ImportCompetition(id);

                    if (_competitionDTO == null)
                    {
                        throw new Exception("504");
                    }

                    //Importamos lo equipos

                    var _teamsLogic = scope.Resolve<Dom.ITeam>();
                    _competitionDTO.Teams = _teamsLogic.ImportTeamsFromCompetition(id);

                    if (_competitionDTO.Teams != null)
                    {

                        int i = 0;///por solo traer 8
                        foreach (TeamDTO team in _competitionDTO.Teams)
                        {
                            if (i == 8)// SOlo importa los jugadores de los primeros 8 equipos
                                break;
                            team.Squad = _playerLogic.ImportPlayersFromTeam(team.Id);
                            i++;
                        }
                    }

                    #endregion importando Conpeticion

                    #region Portando DTO to EF Model
                    //Convertimos DTO a Entity Model
                    var _competitionModelDestino = _mapper.Map<CompetitionModel>(_competitionDTO);
                    _competitionModelDestino.Teams = _mapper.Map<List<TeamDTO>, List<TeamModel>>(_competitionDTO.Teams);

                    #endregion Portando DTO to EF Model

                    #region Guardado Competition

                    //Guardamos la competicion en la DB
                    int _insert = 0;
                    _insert = _competitionLogic.SaveUniqueEntireCompetition(_competitionModelDestino, _playerLogic, _teamLogic, _areaLogic);

                    if (_insert == -1)
                    {
                        //Ya existe la Competicion
                        throw new Exception("429");
                    }
                    else if (_insert == 0)
                    {
                        throw new Exception("0");
                    }

                    #endregion Guardado Competition

                }

            }
            catch (Exception ex)
            {
                int code;
                if (Int32.TryParse(ex.Message, out code))
                {
                    switch (code)
                    {
                        case 400:
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
                        case 403:
                            return Request.CreateResponse(HttpStatusCode.Forbidden, "Restricted Resource");
                        case 404:
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                        case 429:
                            return Request.CreateResponse(HttpStatusCode.Forbidden, "Too Many Requests"); //"Error:429, Too Many Requests";
                        case 409:
                            return Request.CreateResponse(HttpStatusCode.Conflict, "League already imported");
                        case 504:
                            return Request.CreateResponse(HttpStatusCode.GatewayTimeout, "Server Error");
                        case 0:
                            return Request.CreateResponse(HttpStatusCode.Conflict, "No se pudo guardar la competición.");
                        default:
                            return Request.CreateResponse(HttpStatusCode.GatewayTimeout, "Server Error");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, ex.Message);
                }

            }

            /* 
                 HttpCode 201, {"message": "Successfully imported"} --> When the leagueCode was successfully imported.
            o	 HttpCode 409, {"message": "League already imported"} --> If the given leagueCode was already imported into the DB (and in this case, it doesn't need to be imported again).
            o	 HttpCode 404, {"message": "Not found" } --> if the leagueCode was not found.
            o	 HttpCode 504, {"message": "Server Error" } --> If there is any connectivity issue either with the football API or the DB server.
            */
            return Request.CreateResponse(HttpStatusCode.Created, "Successfully imported");
        }
    }
}
