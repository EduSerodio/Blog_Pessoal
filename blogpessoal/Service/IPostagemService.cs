using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Model;

namespace blogpessoal.Service
{
    public interface IPostagemService
    {
        //MÉTODOS CRUD!
        
        Task<IEnumerable<Postagem>> GetAll();

        Task<Postagem?> GetById(long id);

        Task<IEnumerable<Postagem>> GetByTitulo(string titulo);

        Task<Postagem?> Create(Postagem postagem);

        Task<Postagem?> Update(Postagem postagem);

        Task Delete(Postagem postagem);


    }
}