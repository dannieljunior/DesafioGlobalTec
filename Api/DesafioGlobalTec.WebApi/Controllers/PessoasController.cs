using System;
using System.Collections.Generic;
using DesafioGlobalTec.Comum.Exceptions;
using DesafioGlobalTec.Comum.Models;
using DesafioGlobalTec.Repository.Comum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioGlobalTec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoasController : ControllerBase
    {
        private readonly IRepository<Pessoa> _repositorio;

        public PessoasController(IRepository<Pessoa> repositorio)
        {
            _repositorio = repositorio;
        }

        // GET api/pessoas
        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> Get()
        {
            return _repositorio.ObterTodos();
        }

        // GET api/pessoas/{id}
        [HttpGet("{id}")]
        public ActionResult<Pessoa> Get(int id)
        {
            try
            {
                return _repositorio.ObterPorId(id);
            }
            catch(ENotFoundException erro)
            {
                return NotFound(erro.Message);
            }
            catch(Exception erro)
            {
                return BadRequest(erro.Message);
            }            
        }

        // POST api/pessoas
        [HttpPost]
        public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                    return _repositorio.Salvar(pessoa);
                else
                    return BadRequest(ModelState);
            }
            catch (ENotFoundException erro)
            {
                return NotFound(erro.Message);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        // PUT api/pessoas/5
        [HttpPut()]
        public ActionResult<Pessoa> Put([FromBody] Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                    return _repositorio.Salvar(pessoa);
                else
                    return BadRequest(ModelState);
            }
            catch (ENotFoundException erro)
            {
                return NotFound(erro.Message);
            }
            catch(Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        // DELETE api/pessoas/1
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _repositorio.Excluir(id);
                return "Pessoa foi excluída com sucesso.";
            }
            catch (ENotFoundException erro)
            {
                return NotFound(erro.Message);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}