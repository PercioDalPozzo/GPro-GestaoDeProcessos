namespace Aplicacao.Infra
{
    public class IdView
    {
        public int Id { get; set; }

        public static IdView Novo(int id)
        {
            return new IdView() { Id = id };
        }
    }
}
