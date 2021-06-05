using Aplicacao.Infra;
using Repositorio.Contexto;
using System;
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

        public void Salvar(T entidade)
        {
            if (entidade.Id == 0)
                _contexto.Set<T>().Attach(entidade);
            else
                _contexto.Set<T>().Update(entidade);
        }

        public void SaveChanges()
        {
            _contexto.SaveChanges();
        }
    }
}
