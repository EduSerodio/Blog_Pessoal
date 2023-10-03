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
                .MinimumLength(2)   // minumo de caracteres
                .MaximumLength(80);   // maximo de caracteres
        }
    }
}