using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Model;

namespace blogpessoal.Service
{
    public interface ITemaService
    {
        Task<IEnumerable<Tema>> GetAll();

        Task<Tema?> GetById(long id);

        Task<IEnumerable<Tema>> GetByDescricao(string descricao);

        Task<Tema?> Create(Tema tema);

        Task<Tema?> Update(Tema tema);

        Task Delete(Tema tema);
    }
}