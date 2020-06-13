using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMobills.Models;
using DesafioMobills.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioMobills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaRepository despesaRepository;

        public DespesaController(IConfiguration configuration)
        {
            despesaRepository = new DespesaRepository(configuration);
        }


        // GET: api/<DespesaController>
        [HttpGet]
        public ActionResult<IEnumerable<Despesa>> Get()
        {
            var result = despesaRepository.FindAll().ToList();

            return result;
        }

        // GET api/<DespesaController>/5
        [HttpGet("{id}",Name ="AdicionarDespesa")]
        public ActionResult<Despesa> Get(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Despesa despesa = despesaRepository.FindByID(id.Value);
            if (despesa == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return despesa;
        }

        // POST api/<DespesaController>
        [HttpPost]
        public ActionResult<string> Post([FromBody] Despesa despesa)
        {
            var result = despesaRepository.Add(despesa);

            if (result)
            {
                return "Despesa inserida";
            }
            return "Erro ao inserir a despesa";
        }

        // PUT api/<DespesaController>/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] Despesa despesa)
        {
            despesa.Id = id;
            var result = despesaRepository.Update(despesa);

            if (result)
            {
                return "Despesa atualizada";
            }
            return "Erro ao tentar atualizar a despesa";
        }

        // DELETE api/<DespesaController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            var result = despesaRepository.Remove(id);

            if (result)
            {
                return "Despesa Removida";
            }
            return "Erro ao tentar remover a despesa";
        }
    }
}
