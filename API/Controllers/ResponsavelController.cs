using Aplicacao.Aplicacao.Responsavel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    public class ResponsavelController : Controller
    {
        private readonly IAplicResponsavel _aplicResponsavel;


        public ResponsavelController(IAplicResponsavel aplicResponsavel)
        {
            _aplicResponsavel = aplicResponsavel;
        }

        public List<RetornoPesquisarView> Pesquisar(FiltroPesquisarView view)
        {
            try
            {
                return _aplicResponsavel.Pesquisar(view);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}
