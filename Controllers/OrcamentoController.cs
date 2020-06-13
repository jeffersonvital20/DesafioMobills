using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMobills.Models;
using DesafioMobills.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DesafioMobills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {
        private readonly ReceitaRepository receitaRepository;
        private readonly DespesaRepository despesaRepository;

        public OrcamentoController(IConfiguration configuration)
        {
            despesaRepository = new DespesaRepository(configuration);
            receitaRepository = new ReceitaRepository(configuration);
        }

        //Orcamento total
        [Route("total")]
        [HttpGet]
        public ActionResult<Dictionary<string,decimal>> GetOrcamentoTotal()
        {
            var despesas = despesaRepository.FindAll();
            var receitas = receitaRepository.FindAll();

            return CalculoOrcamento(despesas, receitas);
        }
        //Orcamento não contabilizado
        [Route("naopago")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoNaoPago()
        {
            var despesas = despesaRepository.FindByPago(false);
            var receitas = receitaRepository.FindAll();

            return CalculoOrcamento(despesas, receitas);
        }
        //Orcamento Contabilizado
        [Route("naorecebido")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoNaoRecebido()
        {
            var despesas = despesaRepository.FindAll();
            var receitas = receitaRepository.FindByRecebido(false);

            return CalculoOrcamento(despesas, receitas);
        }
        [Route("recebidoepago")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoRecebidoPago()
        {
            var despesas = despesaRepository.FindByPago(true);
            var receitas = receitaRepository.FindByRecebido(true);

            return CalculoOrcamento(despesas, receitas);
        }
        [Route("recebidoenaopago")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoRecebidoNaoPago()
        {
            var despesas = despesaRepository.FindByPago(false);
            var receitas = receitaRepository.FindByRecebido(true);

            return CalculoOrcamento(despesas, receitas);
        }
        [Route("naorecebidoepago")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoNaoRecebidoPago()
        {
            var despesas = despesaRepository.FindByPago(true);
            var receitas = receitaRepository.FindByRecebido(false);

            return CalculoOrcamento(despesas, receitas);
        }
        [Route("naorecebidoenaopago")]
        [HttpGet]
        public ActionResult<Dictionary<string, decimal>> GetOrcamentoNaoRecebidoNaoPago()
        {
            var despesas = despesaRepository.FindByPago(false);
            var receitas = receitaRepository.FindByRecebido(false);

            return CalculoOrcamento(despesas, receitas);
        }

        private Dictionary<string,decimal> CalculoOrcamento(IEnumerable<Despesa> despesas, IEnumerable<Receita> receitas)
        {
            decimal somaDespesa = 0;
            decimal somaReceita = 0;
            foreach (var despesa in despesas)
            {
                somaDespesa += despesa.Valor;

            }
            foreach (var receita in receitas)
            {
                somaReceita += receita.Valor;
            }

            var orcamento = new Dictionary<string, decimal>()
            {
                { "Despesa", somaDespesa},
                { "Receita", somaReceita },
                { "Saldo", somaReceita - somaDespesa }
            };

            return orcamento;
        }

    }
}
