using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        public async Task<Usuario> Alterar(Usuario usuario)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Entry(usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            return usuario;
        }

        public async Task<Usuario> BuscarPorID(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Usuario.FindAsync(id);
            }
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using(GufosContext _contexto = new GufosContext()){
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                return await _contexto.Usuario.ToListAsync();
            }
        }

        public Task<Usuario> Salvar(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        Task<List<Usuario>> IUsuario.Listar()
        {
            throw new System.NotImplementedException();
        }
    }
}