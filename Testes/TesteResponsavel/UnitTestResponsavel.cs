using Aplicacao.Aplicacao.CadastroResponsavel;
using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Dominio.CadastroResponsavel;
using Aplicacao.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testes
{
    [TestClass]
    public class UnitTestResponsavel
    {
        private readonly IValidadorResponsavel _validadorResponsavel;
        private readonly AplicResponsavel _aplicResponsavel;

        Mock<IRepResponsavel> mockRepResponsavel;
        Mock<IRepProcessoResponsavel> mockRepProcessoResponsavel;

        public UnitTestResponsavel()
        {
            mockRepResponsavel = new Mock<IRepResponsavel>();
            mockRepProcessoResponsavel = new Mock<IRepProcessoResponsavel>();


            _validadorResponsavel = new ValidadorResponsavel(mockRepResponsavel.Object, mockRepProcessoResponsavel.Object);
            _aplicResponsavel = new AplicResponsavel(mockRepResponsavel.Object, mockRepProcessoResponsavel.Object, _validadorResponsavel);
        }

        [TestMethod]
        public void Test_NomeNaoInformado()
        {
            // arrange            
            var view = new SalvarResponsavelView();

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Nome do respons�vel deve ser informado.", e.Message);
            }
        }

        [TestMethod]
        public void Test_LimiteNomeResponsavel()
        {
            // arrange       
            var view = new SalvarResponsavelView();
            view.Nome = "Aqui tem 151 caracteres................................................................................................................................";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Nome deve possuir no m�ximo 150 caracteres.", e.Message);
            }
        }

        [TestMethod]
        public void Test_CpfNaoInformado()
        {
            // arrange
            var view = new SalvarResponsavelView();
            view.Nome = "P�rcio";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("CPF do respons�vel deve ser informado.", e.Message);
            }
        }

        [TestMethod]
        public void Test_CpfInvalido()
        {
            // arrange
            var view = new SalvarResponsavelView();
            view.Nome = "P�rcio";
            view.Cpf = "12345678901";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("CPF inv�lido 12345678901.", e.Message);
            }
        }

        [TestMethod]
        public void Test_EmailNaoInformado()
        {
            // arrange
            var view = new SalvarResponsavelView();
            view.Nome = "P�rcio";
            view.Cpf = "73508085007";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("E-mail do respons�vel deve ser informado.", e.Message);
            }
        }

        [TestMethod]
        public void Test_EmailInvalido()
        {
            // arrange
            var view = new SalvarResponsavelView();
            view.Nome = "P�rcio";
            view.Cpf = "73508085007";
            view.Email = "EmailNadaHaver";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("E-mail inv�lido EmailNadaHaver.", e.Message);
            }
        }

        [TestMethod]
        public void Test_CpfDuplicado()
        {
            // arrange
            var view = new SalvarResponsavelView();
            view.Nome = "P�rcio";
            view.Cpf = "73508085007";
            view.Email = "EmailNadaHaver@mesmo";


            var registrosFake = new List<Responsavel>() { new Responsavel() { Id = 1, Nome = "P�rcio", Cpf = new Cpf("73508085007") } };
            mockRepResponsavel.Setup(p => p.Recuperar()).Returns(registrosFake.AsQueryable());

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O CPF j� est� em uso pelo respons�vel P�rcio.", e.Message);
            }
        }

        [TestMethod]
        public void Test_RemocaoComVinculo()
        {
            // arrange
            var view = new IdView();
            view.Id = 1;


            // responsavel
            var responsavel = new Responsavel();
            responsavel.Id = 1;
            mockRepResponsavel.Setup(p => p.Find(1)).Returns(responsavel);

            // vinculo com processo
            var processo1 = new Processo();
            processo1.NumeroProcesso = new NumeroProcesso("0123456789");
            var processo2 = new Processo();
            processo2.NumeroProcesso = new NumeroProcesso("0123456789");

            var registrosFake = new List<ProcessoResponsavel>()
            {
                new ProcessoResponsavel(processo1, responsavel),
                new ProcessoResponsavel(processo2, responsavel)
            };

            mockRepProcessoResponsavel.Setup(p => p.Recuperar()).Returns(registrosFake.AsQueryable());

            // action 
            try
            {
                _aplicResponsavel.Remover(view);
                Assert.Fail("Teste deve gerar exce��o");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O respons�vel n�o pode ser removido pois est� vinculado ao processo 0123456789. Processos vinculados: 2.", e.Message);
            }
        }
    }
}
