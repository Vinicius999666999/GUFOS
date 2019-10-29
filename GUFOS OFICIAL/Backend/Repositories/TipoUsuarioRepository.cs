using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        public async Task<TipoUsuario> Alterar(TipoUsuario tipoUsuario)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Entry(tipoUsuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return tipoUsuario;
        }

        public Task<TipoUsuario> Alterar(Presenca presenca)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TipoUsuario> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.TipoUsuario.FindAsync(id);
            }
        }

        public async Task<TipoUsuario> Excluir(TipoUsuario tipousuario)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.TipoUsuario.Remove(tipousuario);
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }

        public Task<TipoUsuario> Excluir(Presenca presenca)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TipoUsuario>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.TipoUsuario.ToListAsync();
            }
        }

        public async Task<TipoUsuario> Salvar(TipoUsuario tipousuario)
        {
            using(GufosContext _contexto = new GufosContext()){
                await _contexto.AddAsync(tipousuario);
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }

        public Task<TipoUsuario> Salvar(Presenca presenca)
        {
            throw new System.NotImplementedException();
        }
    }
}