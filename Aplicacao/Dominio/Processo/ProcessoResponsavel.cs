using Aplicacao.Infra;

namespace Aplicacao.Dominio
{
    public class ProcessoResponsavel : Entidade
    {
        protected ProcessoResponsavel()
        {
            // necessário para o EntityCore
        }

        public ProcessoResponsavel(Processo processo, Responsavel responsavel)
        {
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
