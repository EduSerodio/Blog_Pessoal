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

        //async para indicar que esse metodo vai ser preenchido na pag
        public async Task<IEnumerable<Postagem>> GetAll()
        {   
            
            return await _context.Postagens
                .Include(p => p.Tema)
                .ToListAsync();
            
        }

        public async Task<Postagem?> GetById(long id)
        {
            try
            {
                var Postagem = await _context.Postagens
                                    .Include(p => p.Tema)
                                    .FirstAsync(p => p.Id == id);

                return Postagem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            var Postagem = await _context.Postagens
                                .Include(p => p.Tema)
                                .Where(p => p.Titulo.Contains(titulo))
                                .ToListAsync();
            return Postagem;
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            if(postagem.Tema is not null)
            {
                var BuscarTema = await _context.Tema.FindAsync(postagem.Tema.Id);
                if(BuscarTema is null)
                    return null;
            }

            //if ternário   
            postagem.Tema = postagem.Tema is not null ? _context.Tema.FirstOrDefault(t => t.Id == postagem.Tema.Id) : null;

            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync();

            return postagem;
        }

         public async Task<Postagem?> Update(Postagem postagem)
        {
            var PostagemUpdate = await _context.Postagens.FindAsync(postagem.Id);

            if(PostagemUpdate is null)
            {
                return null;
            }
            
             if(postagem.Tema is not null)
            {
                var BuscarTema = await _context.Tema.FindAsync(postagem.Tema.Id);
                if(BuscarTema is null)
                    return null;
            }

            //if ternario
            postagem.Tema = postagem.Tema is not null ? _context.Tema.FirstOrDefault(t => t.Id == postagem.Tema.Id) : null;

            _context.Entry(PostagemUpdate).State = EntityState.Detached;
            _context.Entry(postagem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return postagem;
        }

        public async Task Delete(Postagem postagem)
        {
            _context.Remove(postagem);
            await _context.SaveChangesAsync();
        }
    }
}