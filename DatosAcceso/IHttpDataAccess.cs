namespace DatosAcceso {
    public interface IHttpDataAccess {
        T Import<T>(string competitionId);
    }
}