using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Model;
using FluentValidation;

namespace blogpessoal.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {

        // Fazendo a validação da postagem do nosso blog 
        public TemaValidator() {
            
            //validação para a opção titulo da postagem
            RuleFor(t => t.Descricao)
                .NotEmpty()    //nao pode estar vaxio
                .MinimumLength(10)   // minumo de caracteres
                .MaximumLength(10000);   // maximo de caracteres
        }
    }
}