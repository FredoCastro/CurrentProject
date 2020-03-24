using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DatosEntidades;
using DTOEntidades;

namespace Mapas {
    public class AutoMapperConfiguration {

        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings() {
            MapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<AreaDTO, AreaModel>().ReverseMap();
                cfg.CreateMap<CompetitionDTO, CompetitionModel>().ReverseMap(); 
                cfg.CreateMap<PlayerDTO, PlayerModel>().ReverseMap();
                cfg.CreateMap<TeamDTO, TeamModel>().ReverseMap(); 
            });
        }
    }
}