using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
//using ImportLeagueWebAPI.Controllers;
using AutoMapper;
using DatosEntidades;
using DTOEntidades;
using ImportLeagueDominio;
using DatosRepositorio;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Core.Activators.Reflection;
using Autofac.Integration.WebApi;

namespace DirectInjection {
    public static class ContainerConfig {
        public static IContainer Configure(Dictionary<ApiController,Type> apiControllers, Assembly controllersAssembly) {
            var builder = new ContainerBuilder();

            //FOrma estandar de registrar uno por uno
            //builder.RegisterType<CompetitionBusinessLogic>().As<ICompetitionBusinessLogic>();

            //Mapea el Contex data base
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(DatosRepositorio)))
                .Where(t => t.Namespace.Contains("DatosRepositorio"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name)).As<IServiceProvider>().SingleInstance();

            //Mapea las clases con la logica de cada Entidad
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ImportLeagueDominio)))
                .Where(t => t.Namespace.Equals("ImportLeagueDominio"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name)).As<IServiceProvider>();

            //Configuracion circular de automapper y autofac
            builder.Register(ConfigureMapper).SingleInstance();


            #region Controllers
            //register individual controlllers manually.
            //builder.RegisterType<ImportController>().InstancePerRequest();
            //builder.RegisterType<HomeController>().InstancePerRequest();

            //Controllers

            //Funciona si estan en el mismo proyecto
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t)).InstancePerLifetimeScope();

            // Register instances of objects you create...
            foreach (KeyValuePair<ApiController, Type> keyValue in apiControllers)
            {
                //builder.RegisterType<keyValue.Value>().InstancePerRequest();
                //key -> instancia de controller, value -> Tipo que implementa la interfaz
                //                builder.RegisterType(keyValue.Value).FindConstructorsWith
                builder.RegisterType(keyValue.Value).UsingConstructor(typeof(IMapper), typeof(IContainer)).InstancePerRequest();
                
                //builder.Register(c => new ImportController(c.ResolveNamed<IMapper>("Mapper"), c.ResolveNamed<IContainer>("Container")));
            }

            builder.RegisterApiControllers(controllersAssembly);

            #endregion Controllers


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