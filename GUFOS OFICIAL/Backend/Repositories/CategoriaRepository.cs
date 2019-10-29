using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        public async Task<Categoria> Alterar(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Entry(categoria).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return categoria;
        }
        public  async Task<Categoria> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Categoria.FindAsync(id);
            }
        }

        public async Task<Categoria> Excluir(Categoria categoria)
        {
            using(GufosContext _contexto = new GufosContext()){
                 _contexto.Categoria.Remove(categoria);
                 await _contexto.SaveChangesAsync();
                 return categoria;
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Categoria.ToListAsync();
            }
        }
              public async Task<Categoria> Salvar(Categoria categoria)
        {
            using(GufosContext _context = new GufosContext()){
                await _context.AddAsync(categoria);
                await _context.SaveChangesAsync();
                return categoria;
            }
        }
        }
    }
