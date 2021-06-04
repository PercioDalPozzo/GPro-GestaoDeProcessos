using Aplicacao.Aplicacao.Responsavel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class ResponsavelController : Controller
    {
        private readonly IAplicResponsavel _aplicResponsavel;


        public ResponsavelController(IAplicResponsavel aplicResponsavel)
        {
            _aplicResponsavel = aplicResponsavel;
        }

        [HttpGet]
        public RetornoViewModel Foi()
        {
            return RetornoViewModel.RetornoSucesso("Foi..." + DateTime.Now.ToLongTimeString());
        }

        public RetornoViewModel Pesquisar(FiltroPesquisarView view)
        {
            try
            {
                var retorno = _aplicResponsavel.Pesquisar(view);
                return RetornoViewModel.RetornoSucesso(retorno);
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }
    }
}
