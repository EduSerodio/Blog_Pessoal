using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Data;
using blogpessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace blogpessoal.Service.Implements
{
    public class PostagemService : IPostagemService
    {
        //Instanciando 
        private readonly AppDbContext _context;

        //construtor 
        public PostagemService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Postagem?> Create(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        //async para indicar que esse metodo vai ser preenchido na pag
        public async Task<IEnumerable<Postagem>> GetAll()
        {   
            //
            return await _context.Postagens.ToListAsync();
            
        }

        public Task<Postagem?> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Postagem>> GetByTiyulo(string titulo)
        {
            throw new NotImplementedException();
        }

        public Task<Postagem?> Update(Postagem postagem)
        {
            throw new NotImplementedException();
        }
    }
}