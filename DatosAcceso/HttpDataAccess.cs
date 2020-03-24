using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using ImportLeagueWebAPI.Models;
using System.Net.Http;

namespace DatosAcceso {
    public class HttpDataAccess : IHttpDataAccess {

        public T Import<T>(string _id) {

            T _entityModel = default(T);

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(Util.URICompetition());
                client.DefaultRequestHeaders.Add(Util.APITokenName(), Util.APITokenValue());
                //HTTP GET
                var responseTask = client.GetAsync(_id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();
                    _entityModel = readTask.Result;
                } else {
                    throw new Exception(result.StatusCode.ToString());
                }
            }
            return _entityModel;
        }
    }
}