using Aplicacao.Aplicacao.CadastroProcesso;
using Aplicacao.Infra;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IAplicProcesso _aplicProcesso;


        public ProcessoController(IAplicProcesso aplicProcesso)
        {
            _aplicProcesso = aplicProcesso;
        }

        public RetornoViewModel PrepararEdicao(IdView view)
        {
            try
            {
                var retorno = _aplicProcesso.PrepararEdicao(view);
                return RetornoViewModel.RetornoSucesso(retorno);
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }

        public RetornoViewModel Pesquisar(FiltroPesquisarView view)
        {
            try
            {
                var retorno = _aplicProcesso.Pesquisar(view);
                return RetornoViewModel.RetornoSucesso(retorno);
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }

        public RetornoViewModel Salvar(SalvarProcessoView view)
        {
            try
            {
                var retorno = _aplicProcesso.Salvar(view);
                return RetornoViewModel.RetornoSucesso(retorno);
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }

        public RetornoViewModel Remover(IdView view)
        {
            try
            {
                _aplicProcesso.Remover(view);
                return RetornoViewModel.RetornoSucesso();
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }


    }
}
