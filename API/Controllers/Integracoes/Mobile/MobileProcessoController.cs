using Aplicacao.Aplicacao.CadastroProcesso;
using Aplicacao.Infra;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("MobileProcesso")]
    public class MobileProcessoController : Controller
    {
        private readonly IAplicProcesso _aplicProcesso;

        public MobileProcessoController(IAplicProcesso aplicProcesso)
        {
            _aplicProcesso = aplicProcesso;
        }

        [HttpPost("PrepararEdicao")]
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

        [HttpPost("Pesquisar")]
        public RetornoViewModel Pesquisar(FiltroPesquisarProcessoView view)
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

        [HttpPost("Salvar")]
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

        [HttpPost("Remover")]
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
