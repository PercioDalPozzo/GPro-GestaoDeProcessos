using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Dominio.Responsavel
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public byte[] Foto { get; set; }
    }
}
