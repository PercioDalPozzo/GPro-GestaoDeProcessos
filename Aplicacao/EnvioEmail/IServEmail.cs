using System.Threading.Tasks;

namespace Aplicacao.EnvioEmail
{
    public interface IServEmail
    {
        Task EnviarEmail(EnviarEmailView view);
    }
}
