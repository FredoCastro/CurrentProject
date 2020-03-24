using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entity = DatosEntidades;
using DatosRepositorio;
using System.Linq.Expressions;

namespace ImportLeagueDominio
{
    public class Area : IArea
    {
        //
        private IRepositorio<entity.IAreaModel> _repositorio;

        //Declaramos el contexto en el repositorio para que direct injection lo agregue 
        public Area(IRepositorio<entity.IAreaModel> iImportLeagueContex)
        {
            _repositorio = iImportLeagueContex;
        }

        public void Adicionar(entity.IAreaModel entidad)
        {
            _repositorio.Adicionar(entidad);
            _repositorio.Grabar();
        }

        public void Modificar(entity.IAreaModel entidad)
        {
            _repositorio.Modificar(entidad);
            _repositorio.Grabar();
        }

        public void Eliminar(entity.IAreaModel entidad)
        {
            _repositorio.Eliminar(entidad);
            _repositorio.Grabar();
        }

        public IEnumerable<entity.IAreaModel> TraerTodo()
        {
            return _repositorio.TraerTodo().ToList();
        }

        public entity.IAreaModel TraerUnoPorId(int Id)
        {
            return _repositorio.TraerUnoPorId(Id);
        }

        public entity.IAreaModel TraerUno(Expression<Func<entity.IAreaModel, bool>> predicado)
        {
            return _repositorio.TraerUno(predicado);
        }
    }
}
