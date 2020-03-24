using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DatosEntidades;

namespace ImportLeagueDominio
{
    public interface IArea
    {
        void Adicionar(IAreaModel entidad);
        void Eliminar(IAreaModel entidad);
        void Modificar(IAreaModel entidad);
        IEnumerable<IAreaModel> TraerTodo();
        IAreaModel TraerUno(Expression<Func<IAreaModel, bool>> predicado);
        IAreaModel TraerUnoPorId(int Id);
    }
}