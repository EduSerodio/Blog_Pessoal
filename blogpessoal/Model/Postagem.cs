using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogpessoal.Model
{
    public class Postagem : Auditable
    {
        [Key] // pRIMARY kEY (Id)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // IDENTITY(1,1)
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Titulo { get; set ;} = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Texto {get; set;} = string.Empty;

        //Criando o Relacionamento entre clases 
        public virtual Tema? Tema {get; set;}
        public virtual User? Usuario { get; set; }
    }
}