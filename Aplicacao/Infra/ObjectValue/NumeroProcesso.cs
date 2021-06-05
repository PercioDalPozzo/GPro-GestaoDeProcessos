﻿using System;
using System.Text.RegularExpressions;

namespace Aplicacao.Infra.ObjectValue
{
    public class NumeroProcesso
    {
        public string Value { get; set; }

        public NumeroProcesso()
        {

        }

        public NumeroProcesso(string numeroProcesso)
        {
            Value = numeroProcesso;
        }

        public static string RemoverFormatacao(string numero)
        {
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            var regex = new Regex(pattern);
            return regex.Replace(numero, "");
        }

        public static string Formatar(string numero)
        {
            //NNNNNNN-DD.AAAA.JTR.OOOO
            if (numero.Length == 20)
                return numero.Insert(7, "-").Insert(10, ".").Insert(15, ".").Insert(19, ".");

            return numero;

        }

        public void Validar()
        {
            if (!EhValido())
                throw new Exception(string.Format("Número processo unificado {0}.", Value));
        }

        public bool EhValido()
        {
            return (Value.Length == 20);
        }
    }
}
