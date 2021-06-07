using Aplicacao.Infra;
using Repositorio.Contexto;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepBase<T> where T : Entidade
    {
        protected readonly ContextoBanco _contexto;

        public RepBase(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public T Find(int id)
        {
            return _contexto.Set<T>().Find(id);
        }

        public IQueryable<T> Recuperar()
        {
            return _contexto.Set<T>();
        }

        public void Remover(T entidade)
        {
            _contexto.Set<T>().Remove(entidade);
        }

        public void Attach(T entidade)
        {
            _contexto.Set<T>().Attach(entidade);
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
