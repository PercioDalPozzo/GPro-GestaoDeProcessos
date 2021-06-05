namespace Aplicacao.Infra
{
    public class RetornoSalvarView
    {
        public RetornoSalvarView(Entidade entidade)
        {
            Id = entidade.Id;
        }

        public int Id { get; set; }
    }
}
