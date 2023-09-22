using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Model;
using blogpessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace blogpessoal.Controller
{
    [Route("~/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        //injetando as validações e a interface para pegar os métodos crud 
        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;

        //construtor
        public PostagemController(IPostagemService postagemService, IValidator<Postagem> postagemValidator)
        {
            _postagemService = postagemService;
            _postagemValidator = postagemValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(){

            return Ok(await _postagemService.GetAll());
        }
    }
}