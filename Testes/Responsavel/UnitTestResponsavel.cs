using Aplicacao.Aplicacao;
using Aplicacao.Aplicacao.CadastroResponsavel;
using Aplicacao.Dominio;
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
            string msg = "";
            var view = new SalvarResponsavelView();

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("Nome do responsável deve ser informado.", msg);
        }

        [TestMethod]
        public void Test_LimiteNomeResponsavel()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Aqui tem 151 caracteres................................................................................................................................";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("Nome deve possuir no máximo 150 caracteres.", msg);
        }

        [TestMethod]
        public void Test_CpfNaoInformado()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Pércio";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("CPF do responsável deve ser informado.", msg);
        }

        [TestMethod]
        public void Test_CpfInvalido()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Pércio";
            view.Cpf = "12345678901";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("CPF inválido 12345678901.", msg);
        }

        [TestMethod]
        public void Test_EmailNaoInformado()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Pércio";
            view.Cpf = "73508085007";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("E-mail do responsável deve ser informado.", msg);
        }

        [TestMethod]
        public void Test_EmailInvalido()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Pércio";
            view.Cpf = "73508085007";
            view.Email = "EmailNadaHaver";

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("E-mail inválido EmailNadaHaver.", msg);
        }

        [TestMethod]
        public void Test_CpfDuplicado()
        {
            // arrange
            string msg = "";
            var view = new SalvarResponsavelView();
            view.Nome = "Pércio";
            view.Cpf = "73508085007";
            view.Email = "EmailNadaHaver@mesmo";


            var registrosFake = new List<Responsavel>() { new Responsavel() { Id = 1, Nome = "Pércio", Cpf = new Cpf("73508085007") } };
            mockRepResponsavel.Setup(p => p.Recuperar()).Returns(registrosFake.AsQueryable());

            // action 
            try
            {
                _aplicResponsavel.Salvar(view);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("O CPF já está em uso pelo responsável Pércio.", msg);
        }

        [TestMethod]
        public void Test_RemocaoComVinculo()
        {
            // arrange
            string msg = "";
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
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            // assert
            Assert.AreEqual("O responsável não pode ser removido pois está vinculado ao processo 0123456789. Processos vinculados: 2.", msg);
        }


    }
}
