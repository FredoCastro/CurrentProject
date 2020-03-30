using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.WebApi;
using DirectInjection;
using System.Reflection;
using ImportLeagueWebAPI.Controllers;
using System.Web.Mvc;

namespace ImportLeagueWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ImportLeagueWebAPI",
                routeTemplate: "import-league/{id}"
                , defaults: new { controller = "import", id = RouteParameter.Optional }
            );

            //Dependency Injection
            //Enviamos como parametro los controladores con su tipo para mapearlos
            //Dictionary<ApiController, Type> apiControllers = new Dictionary<ApiController, Type>();

            //var _controllers = Assembly.Load(nameof(ImportLeagueWebAPI)).GetTypes().Where(t => t.Name.Contains("Controller")); //.Select( y => y = new KeyValuePair< new ImportController(), ImportController>());  //.Select(x=>x = new KeyValuePair<   ,t>());

            //foreach (var _controller in _controllers) {

            //    //Da error 
            //    //var _apiController = (ApiController)Activator.CreateInstance(_controller.GetType(),true);


            //    ApiController _apiController = null;

            //    object[] param = new object[0];
            //    Type[] typeParam = new Type[0];

            //    if (_controller.IsClass && _controller.BaseType.Name == "ApiController")
            //    {
            //        _apiController = (ApiController)_controller.GetConstructor(typeParam).Invoke(param);
            //    }

            //    if (_apiController != null)
            //    {
            //        //Con interfaces
            //        //apiControllers.Add(_apiController, ObtenerInterfaceImplementada(_apiController));

            //        //Prueba con la clase del controlador
            //        apiControllers.Add(_apiController, _apiController.GetType());
            //    }
            //    //CrearParObjetoTipo(Activator.CreateInstance(_controller.GetType()));
            //}

            //Assembly _executingAssembly = Assembly.GetExecutingAssembly();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(ContainerConfig.Configure());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Devuelve la interfaz implementada por un objeto Controller</returns>
        static Type ObtenerInterfaceImplementada<T>(T _object) where T : class
        {
            Type _interface = _object.GetType().GetInterfaces().Where(x => x.Name.Contains(_object.GetType().Name)).FirstOrDefault();
            return _interface;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_object"></param>
        /// <returns>par clave valor donde clave es una instancia de un objeto Controller y el valor la interface que este implementa </returns>
        static KeyValuePair<T, Type> CrearParObjetoTipo<T>(T _object) where T : class
        {
            Type _interface = _object.GetType().GetInterfaces().Where(x => x.Name.Contains("Controller")).FirstOrDefault();

            KeyValuePair<T, Type> _o = new KeyValuePair<T, Type>();

            if (_interface != null)
            {
                _o = new KeyValuePair<T, Type>(_object, _interface);
            }
            else
            {
                _o = new KeyValuePair<T, Type>(_object, _object.GetType());
            }

            return _o;
        }
    }
}
