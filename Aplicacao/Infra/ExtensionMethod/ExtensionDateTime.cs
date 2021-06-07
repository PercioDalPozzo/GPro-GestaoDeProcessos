using System;

namespace Aplicacao.Infra
{
    public static class ExtensionDateTime
    {
        public static string NossoFormato(this DateTime dataTime)
        {
            return dataTime.ToString("dd/MM/yyyy");
        }
    }
}
