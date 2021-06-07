using Aplicacao.Dominio.CadastroResponsavel;
using Aplicacao.Infra;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public class ProcessoResponsavel : Entidade
    {
        protected ProcessoResponsavel()
        {
            // necessário para o EntityCore
        }

        public ProcessoResponsavel(Processo processo, Responsavel responsavel)
        {
            processo.ProcessoResponsavel.Add(this);

            Processo = processo;
            CodigoProcesso = processo.Id;

            Responsavel = responsavel;
            CodigoResponsavel = responsavel.Id;
        }

        public int CodigoProcesso { get; private set; }
        public int CodigoResponsavel { get; private set; }

        public virtual Processo Processo { get; set; }
        public virtual Responsavel Responsavel { get; set; }
    }
}
