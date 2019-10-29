using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class EventoRepository : IEvento
    {
        public async Task<Evento> Alterar(Evento evento)
        {
            using(GufosContext _context = new GufosContext()){
                _context.Entry(evento).State = EntityState.Modified; 
                await _context.SaveChangesAsync();
            }
            return evento;
        }

        public async Task<Evento> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Evento.FindAsync(id);
            }
        }

        public async Task<Evento> Excluir(Evento evento)
        {
            using(GufosContext _context = new GufosContext()){
                _context.Evento.Remove(evento);
                await _context.SaveChangesAsync();
                return evento;
            }
        }

        public async Task<List<Evento>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Evento.ToListAsync();
            }
        }

        internal Task BurcarPorID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Evento> Salvar(Evento evento)
        {
            using(GufosContext _context = new GufosContext()){
                await _context.AddAsync(evento);
                await _context.SaveChangesAsync();
                return evento;
            }
        }
    }

    public interface IEvento
    {
    }
}