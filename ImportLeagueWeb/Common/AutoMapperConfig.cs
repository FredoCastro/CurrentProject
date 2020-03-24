using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ImportLeagueWebAPI.Models;
using ImportLeagueWebAPI.DTObjects;
using Autofac;

namespace ImportLeagueWebAPI {
    public class AutoMapperConfiguration {

        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings() {
            MapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<AreaDTO, AreaModel>().ReverseMap();
                //cfg.CreateMap<CompetitionDTO, CompetitionModel>().ForMember<ICollection<TeamModel>>(destino=>destino.Teams, map=>map.MapFrom(origen=>origen.Teams)).ForMember<string>(destino=>destino.AreaName, origen=>origen.Ignore()).ReverseMap();
                //cfg.CreateMap<CompetitionModel, CompetitionDTO > ().ForMember<ICollection<TeamDTO>>(destino => destino.Teams, map => map.MapFrom(origen => origen.Teams)).ForMember<string>(destino => destino.AreaName, origen => origen.Ignore()).ReverseMap();
                cfg.CreateMap<CompetitionDTO, CompetitionModel>().ReverseMap(); // .ForMember(destino => destino.Teams, origen => origen.Ignore()).IncludeMembers(member=>member.Area).ReverseMap();
                //cfg.CreateMap<AreaDTO, CompetitionModel>(MemberList.None);
                //cfg.CreateMap<CompetitionModel, CompetitionDTO>().ForMember(destino => destino.Teams, origen => origen.Ignore()).ForMember(destino => destino.AreaName, origen => origen.Ignore());
                cfg.CreateMap<PlayerDTO, PlayerModel>().ReverseMap();
                cfg.CreateMap<TeamDTO, TeamModel>().ReverseMap(); //.ForMember(destino => destino.AreaName, map => map.MapFrom(origen => origen.Area.Name));
                //cfg.CreateMap<TeamModel, TeamDTO>().ForMember(destino => destino.AreaName, map => map.MapFrom(origen => origen.Area.Name));
            });
        }


    }
}