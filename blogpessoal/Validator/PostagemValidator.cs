using blogpessoal.Model;
using FluentValidation;

namespace blogpessoal.Validator
{
    public class PostagemValidator : AbstractValidator<Postagem>
    {
        // Fazendo a validação da postagem do nosso blog 
        public PostagemValidator() {
            
            //validação para a opção titulo da postagem
            RuleFor(p => p.Titulo)
                .NotEmpty()    //nao pode estar vaxio
                .MinimumLength(5)   // minumo de caracteres
                .MaximumLength(100);   // maximo de caracteres

            RuleFor(p => p.Texto)
                .NotEmpty()    //nao pode estar vaxio
                .MinimumLength(10)  // minumo de caracteres
                .MaximumLength(8000);  // maximo de caracteres
 
        }
    }
}