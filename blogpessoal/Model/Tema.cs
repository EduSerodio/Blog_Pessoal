
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogpessoal.Model
{
    public class Tema
    {
        [Key] // pRIMARY kEY (Id)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // IDENTITY(1,1)
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        //Criando o Relacionamento entre clases (objeto da classe postagem)
        [InverseProperty("Tema")]
        public virtual ICollection<Postagem>? Postagem { get; set; }
    }
}