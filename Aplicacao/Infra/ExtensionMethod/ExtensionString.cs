using System.Text.RegularExpressions;

namespace Aplicacao.Infra
{
    public static class ExtensionString
    {
        public static string SomenteNumeros(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return "";

            return string.Join("", Regex.Split(texto, @"[^\d]"));
        }
    }
}
