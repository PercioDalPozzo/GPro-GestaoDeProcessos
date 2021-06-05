using System;

namespace Aplicacao.Infra
{
    public class Cpf
    {
        public string Value { get; set; }

        public Cpf()
        {

        }

        public Cpf(string cpf)
        {
            Value = cpf;
        }

        public void Validar()
        {
            if (!EhValido())
                throw new Exception(string.Format("CPF inválido {0}.", Value));
        }

        bool EhValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            Value = Value.Trim();
            Value = Value.Replace(".", "").Replace("-", "");
            if (Value.Length != 11)
                return false;
            tempCpf = Value.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return Value.EndsWith(digito);
        }
    }
}
