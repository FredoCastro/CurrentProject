using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DatosRepositorio {
    public class Repositorio<T>:IRepositorio<T>,IDisposable where T : class{
        private readonly System.Data.Entity.DbContext _context;

        public Repositorio(DbContext dbContext) {
            _context = dbContext;
        }

        public IQueryable<T> AsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Adicionar(T entidad) {
            if (_context.Entry(entidad).State != EntityState.Detached){
                _context.Entry(entidad).State = EntityState.Added;
            }else {
                _context.Set<T>().Add(entidad);
            }
        }
        
        public IEnumerable<T> Buscar(Expression<Func<T, bool>> predicado) {
            return _context.Set<T>().Where(predicado);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public void Eliminar(T entidad) {
            throw new NotImplementedException();
        }

        public void Grabar() {
            _context.SaveChanges();
        }

        public void Modificar(T entidad) {
            if (_context.Entry(entidad).State == EntityState.Detached){
                _context.Set<T>().Add(entidad);
            }
            else {
                _context.Entry(entidad).State = EntityState.Modified;
            }
        }

        public IEnumerable<T> TraerTodo() {
            return _context.Set<T>();
        }

        public T TraerUno(Expression<Func<T, bool>> predicado) {
            return _context.Set<T>().Where(predicado).FirstOrDefault();
        }

        public T TraerUnoPorId(int id) {
            return _context.Set<T>().Find(id);
        }
    }
}
