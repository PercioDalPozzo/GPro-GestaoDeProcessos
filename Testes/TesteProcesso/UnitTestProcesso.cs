using Aplicacao.Aplicacao.CadastroProcesso;
using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Dominio.CadastroResponsavel;
using Aplicacao.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testes.TesteProcesso
{
    [TestClass]
    public class UnitTestProcesso
    {
        private readonly IValidadorProcesso _validadorProcesso;
        private readonly AplicProcesso _aplicProcesso;

        Mock<IRepProcesso> mockRepProcesso;
        Mock<IRepResponsavel> mockRepResponsavel;
        Mock<IRepProcessoResponsavel> mockRepProcessoResponsavel;

        public UnitTestProcesso()
        {
            mockRepProcesso = new Mock<IRepProcesso>();
            mockRepResponsavel = new Mock<IRepResponsavel>();
            mockRepProcessoResponsavel = new Mock<IRepProcessoResponsavel>();

            _validadorProcesso = new ValidadorProcesso(mockRepProcesso.Object);
            _aplicProcesso = new AplicProcesso(mockRepProcesso.Object, mockRepResponsavel.Object, mockRepProcessoResponsavel.Object, _validadorProcesso);
        }

        [TestMethod]
        public void Test_RemocaoProcessoFinalizado()
        {
            // arrange
            var view = new IdView();
            var processo = new Processo() { Situacao = EnumSituacaoProcesso.Finalizado };

            mockRepProcesso.Setup(p => p.Find(view.Id)).Returns(processo);

            try
            {
                // action 
                _aplicProcesso.Remover(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo está finalizado e não poderá ser removido.", e.Message);
            }
        }

        [TestMethod]
        public void Test_RemocaoProcessoArquivado()
        {
            // arrange
            var view = new IdView();
            var processo = new Processo() { Situacao = EnumSituacaoProcesso.Arquivado };

            mockRepProcesso.Setup(p => p.Find(view.Id)).Returns(processo);

            try
            {
                // action 
                _aplicProcesso.Remover(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo está finalizado e não poderá ser removido.", e.Message);
            }
        }

        [TestMethod]
        public void Test_EdicaoProcessoFinalizado()
        {
            // arrange            
            var view = new SalvarProcessoView { Id = 1 };
            var processo = new Processo() { Situacao = EnumSituacaoProcesso.Finalizado };

            mockRepProcesso.Setup(p => p.Find(view.Id)).Returns(processo);

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo está finalizado e não poderá ser editado.", e.Message);
            }

        }

        [TestMethod]
        public void Test_EdicaoProcessoArquivado()
        {
            // arrange
            var view = new SalvarProcessoView { Id = 1 };
            var processo = new Processo() { Situacao = EnumSituacaoProcesso.Arquivado };

            mockRepProcesso.Setup(p => p.Find(view.Id)).Returns(processo);

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo está finalizado e não poderá ser editado.", e.Message);
            }
        }



        [TestMethod]
        public void Test_SalvarSemNumeroProcesso()
        {
            // arrange            
            var view = new SalvarProcessoView();

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Numero do processo unificado deve ser informado.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComNumeroProcessoInvalido()
        {
            // arrange
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "0123456789";

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Número processo unificado inválido 0123456789.", e.Message);
            }
        }


        [TestMethod]
        public void Test_SalvarComNumeroDuplicado()
        {
            // arrange            
            var view = new SalvarProcessoView();
            view.Id = 2;
            view.NumeroProcesso = "01234567890123456789";
            view.PastaFisica = "Pasta";
            view.DataDistribuicao = new DateTime(2021, 06, 08);


            var processo = new Processo();
            mockRepProcesso.Setup(p => p.Find(view.Id)).Returns(processo);

            var registrosFake = new List<Processo>() { new Processo() { Id = 1, NumeroProcesso = new NumeroProcesso("01234567890123456789"), PastaFisica = "Pasta", DataDistribuicao = new DateTime(2021, 06, 08) } };
            mockRepProcesso.Setup(p => p.Recuperar()).Returns(registrosFake.AsQueryable());

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O número do processo unificado 0123456-78.9012.345.6789 pertence a outro processo. (Pasta física: Pasta / Data Distribuição: 08/06/2021).", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComDataSuperior()
        {
            // arrange
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";
            view.DataDistribuicao = DateTime.Today.AddDays(1);

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Data de distribuição se informada, deve ser menor ou igual a data atual.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComPastaMaiorQue50Caracteres()
        {
            // arrange            
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";
            view.PastaFisica = "Aqui tem 51 caracteres.............................";

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Pasta física se informada, deve possuir no máximo 50 caracteres.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComDescricaoMaiorQue1000Caracateres()
        {
            // arrange

            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";
            view.Descricao = "Aqui tem 1001 caracteres.................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................";

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("Descrição se informada, deve possuir no máximo 1000 caracteres.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarSemResponsavel()
        {
            // arrange            
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo deve possuir ao menos um responsável.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComMaisDe3Responsaveis()
        {
            // arrange            
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";
            view.ProcessoResponsavel = new List<SalvarProcessoResponsavelView>
            {
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=1},
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=2},
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=3},
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=4},
            };

            mockRepResponsavel.Setup(p => p.Find(1)).Returns(new Responsavel() { Id = 1 });
            mockRepResponsavel.Setup(p => p.Find(2)).Returns(new Responsavel() { Id = 2 });
            mockRepResponsavel.Setup(p => p.Find(3)).Returns(new Responsavel() { Id = 3 });
            mockRepResponsavel.Setup(p => p.Find(4)).Returns(new Responsavel() { Id = 4 });

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo deve possuir até 3 responsáveis.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComResponsavelDuplicado()
        {
            // arrange            
            var view = new SalvarProcessoView();
            view.NumeroProcesso = "01234567890123456789";
            view.ProcessoResponsavel = new List<SalvarProcessoResponsavelView>
            {
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=1},
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=1},
                new SalvarProcessoResponsavelView(){ CodigoResponsavel=3},
            };

            mockRepResponsavel.Setup(p => p.Find(1)).Returns(new Responsavel() { Id = 1, Nome = "Pércio" });
            mockRepResponsavel.Setup(p => p.Find(3)).Returns(new Responsavel() { Id = 3 });

            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O responsável Pércio está duplicado no processo.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComProcessoDuplicadoNaHierarquia()
        {
            // arrange           
            var view = new SalvarProcessoView();
            view.Id = 2;
            view.NumeroProcesso = "01234567890123456789";
            view.ProcessoResponsavel = new List<SalvarProcessoResponsavelView>
            {
                new SalvarProcessoResponsavelView(),
            };
            mockRepResponsavel.Setup(p => p.Find(It.IsAny<int>())).Returns(new Responsavel());


            var avo = new Processo() { Id = 1 };// duplicado com o neto
            var pai = (new Processo() { Id = 2 });
            var filho = (new Processo() { Id = 3 });
            var neto = (new Processo() { Id = 1 });// duplicado com o avo


            neto.ProcessoPai = filho;
            filho.ProcessoPai = pai;
            pai.ProcessoPai = avo;


            avo.ProcessoFilho.Add(pai);
            pai.ProcessoFilho.Add(filho);
            filho.ProcessoFilho.Add(neto);

            mockRepProcesso.Setup(p => p.Find(2)).Returns(pai);


            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("O processo já faz parte da hierarquia.", e.Message);
            }
        }

        [TestMethod]
        public void Test_SalvarComHierarquiaMaiorQue4()
        {
            // arrange           
            var view = new SalvarProcessoView();
            view.Id = 2;
            view.NumeroProcesso = "01234567890123456789";
            view.ProcessoResponsavel = new List<SalvarProcessoResponsavelView>
            {
                new SalvarProcessoResponsavelView(),
            };
            mockRepResponsavel.Setup(p => p.Find(It.IsAny<int>())).Returns(new Responsavel());


            var avo = new Processo() { Id = 1 };
            var pai = (new Processo() { Id = 2 });
            var filho = (new Processo() { Id = 3 });
            var neto = (new Processo() { Id = 4 });
            var bis = (new Processo() { Id = 5 });


            bis.ProcessoPai = neto;
            neto.ProcessoPai = filho;
            filho.ProcessoPai = pai;
            pai.ProcessoPai = avo;


            avo.ProcessoFilho.Add(pai);
            pai.ProcessoFilho.Add(filho);
            filho.ProcessoFilho.Add(neto);
            neto.ProcessoFilho.Add(bis);

            mockRepProcesso.Setup(p => p.Find(2)).Returns(pai);


            try
            {
                // action 
                _aplicProcesso.Salvar(view);
                Assert.Fail("Teste deve gerar exceção");
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual("A ligação com o processo vinculado irá exceder o limite de 4 processos na hierarquia.", e.Message);
            }
        }
    }
}


