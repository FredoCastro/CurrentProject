using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImportLeagueWebAPI.BusinessLogic;
using System.Reflection;
using ImportLeagueWebAPI.Controllers;
using AutoMapper;
using ImportLeagueWebAPI.Models;
using ImportLeagueWebAPI.DTObjects;

namespace ImportLeagueWebAPI {
    public static class ContainerConfig {
        public static IContainer Configure() {
            var builder = new ContainerBuilder();

            //FOrma estandar de registrar uno por uno
            //builder.RegisterType<CompetitionBusinessLogic>().As<ICompetitionBusinessLogic>();

            //Mapea el Contex data base
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ImportLeagueWebAPI)))
                .Where(t => t.Namespace.Contains("ImportLeagueDataLayer"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name)).SingleInstance();

            //Mapea las clases con la logica de cada Entidad
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ImportLeagueWebAPI)))
                .Where(t => t.Namespace.Contains("BusinessLogic"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            //register individual controlllers manually.
            builder.RegisterType<ImportController>().InstancePerRequest();
            builder.RegisterType<HomeController>().InstancePerRequest();

            //Configuracion circular de automapper y autofac
            builder.Register(ConfigureMapper).SingleInstance();

            return builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IMapper ConfigureMapper(IComponentContext context) {
            var configuration = new MapperConfiguration(config =>
            {
                var ctx = context.Resolve<IComponentContext>();
                config.CreateMap<CompetitionModel, CompetitionDTO>()
                    .ConstructUsing(_ => ctx.Resolve<CompetitionDTO>()).ReverseMap();
                config.CreateMap<AreaModel, AreaDTO>()
                    .ConstructUsing(_ => ctx.Resolve<AreaDTO>()).ReverseMap();
                config.CreateMap<TeamModel, TeamDTO>()
                    .ConstructUsing(_ => ctx.Resolve<TeamDTO>()).ReverseMap();
                config.CreateMap<PlayerModel, PlayerDTO>()
                    .ConstructUsing(_ => ctx.Resolve<PlayerDTO>()).ReverseMap();
            });

            return configuration.CreateMapper();
        }
    }

}