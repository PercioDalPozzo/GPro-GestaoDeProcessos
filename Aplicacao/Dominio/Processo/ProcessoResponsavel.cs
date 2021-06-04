namespace Aplicacao.Dominio.Responsavel
{
    public class ProcessoResponsavel
    {
        protected ProcessoResponsavel()
        {
            // necessário para o EntityCore
        }

        public ProcessoResponsavel(Processo processo)
        {
            Processo = processo;
            CodigoProcesso = processo.Id;
        }

        public int Id { get; set; }
        public int CodigoProcesso { get; set; }
        public int CodigoResponsavel { get; set; }

        public virtual Processo Processo { get; set; }
    }
}
