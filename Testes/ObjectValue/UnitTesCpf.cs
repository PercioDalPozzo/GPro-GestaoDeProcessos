using Aplicacao.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testes.ObjectValue
{
    [TestClass]
    public class UnitTesCpf
    {

        [TestMethod]
        public void Test_CpfInvalido()
        {
            // arrange

            try
            {
                // action 
                new Cpf("123456789").Validar();
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("CPF inválido 123456789.", e.Message);
            }
        }

        [TestMethod]
        public void Test_CpfValidoNaoDeveDarErro()
        {
            // arrange

            // action 
            new Cpf("50247125954").Validar();

            // assert            
        }
    }
}
