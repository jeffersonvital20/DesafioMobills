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
    public class ReceitasController : ControllerBase
    {
        private readonly ReceitaRepository receitaRepository;

        public ReceitasController(IConfiguration configuration)
        {
            receitaRepository = new ReceitaRepository(configuration);
        }


        // GET: api/<ReceitasController>
        [HttpGet]
        public ActionResult<IEnumerable<Receita>> Get()
        {
            var result = receitaRepository.FindAll().ToList();

            return result;
        }

        // GET api/<ReceitasController>/5
        [HttpGet("{id}")]
        public ActionResult<Receita> Get(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Receita receita = receitaRepository.FindByID(id.Value);
            if (receita == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return receita;
        }

        // POST api/<ReceitasController>
        [HttpPost]
        public ActionResult<string> Post([FromBody] Receita receita)
        {
            var result = receitaRepository.Add(receita);

            if (result)
            {
                return "Receita inserida";
            }
            return "Erro ao inserir a receita";
        }

        // PUT api/<ReceitasController>/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] Receita receita)
        {
            receita.Id = id;
            var result = receitaRepository.Update(receita);

            if (result)
            {
                return "Receita atualizada";
            }
            return "Erro ao tentar atualizar a receita";
        }

        // DELETE api/<ReceitasController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            var result = receitaRepository.Remove(id);

            if (result)
            {
                return "Receita Removida";
            }
            return "Erro ao tentar remover a receita";
        }
    }
}
