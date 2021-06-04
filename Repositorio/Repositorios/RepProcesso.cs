using Aplicacao.Dominio.Responsavel;
using Repositorio.Contexto;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepProcesso : IRepProcesso
    {
        private readonly ContextoBanco _contexto;

        public RepProcesso(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public Processo Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Processo> Recuperar()
        {
            return _contexto.Set<Processo>();
        }

        public void Salvar(Processo processo)
        {
            throw new NotImplementedException();
        }
    }
}
