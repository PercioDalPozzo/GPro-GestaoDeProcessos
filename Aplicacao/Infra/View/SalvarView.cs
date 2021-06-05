namespace Aplicacao.Infra
{
    public class SalvarView
    {
        public int Id { get; set; }

        public bool Inserindo()
        {
            return Id == 0;
        }
    }
}
