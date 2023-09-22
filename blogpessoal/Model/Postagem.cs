using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

    }
}