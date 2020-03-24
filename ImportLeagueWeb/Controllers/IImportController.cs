using System.Net.Http;

namespace ImportLeagueWebAPI.Controllers
{
    public interface IImportController
    {
        HttpResponseMessage Get(string id);
    }
}