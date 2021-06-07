﻿using Aplicacao.Aplicacao.CadastroResponsavel;
using Aplicacao.Infra;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("Responsavel")]
    public class ResponsavelController : Controller
    {
        private readonly IAplicResponsavel _aplicResponsavel;


        public ResponsavelController(IAplicResponsavel aplicResponsavel)
        {
            _aplicResponsavel = aplicResponsavel;
        }

        [HttpGet("Ping")]
        public RetornoViewModel Ping()
        {
            return RetornoViewModel.RetornoSucesso("Ping..." + DateTime.Now.ToLongTimeString());
        }

        [HttpPost("PrepararEdicao")]
        public RetornoViewModel PrepararEdicao(IdView view)
        {
            try
            {
                var retorno = _aplicResponsavel.PrepararEdicao(view);
                return RetornoViewModel.RetornoSucesso(retorno);
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }

        [HttpPost("Pesquisar")]
        public RetornoViewModel Pesquisar(FiltroPesquisarResponsavelView view)
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

        [HttpPost("Salvar")]
        public RetornoViewModel Salvar(SalvarResponsavelView view)
        {
            try
            {
                var retorno = _aplicResponsavel.Salvar(view);
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
                _aplicResponsavel.Remover(view);
                return RetornoViewModel.RetornoSucesso();
            }
            catch (Exception e)
            {
                return RetornoViewModel.RetornoErro(e.Message);
            }
        }
    }
}
