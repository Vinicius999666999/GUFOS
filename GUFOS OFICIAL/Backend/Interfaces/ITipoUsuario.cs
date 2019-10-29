using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface ITipoUsuario
    {
        Task<List<TipoUsuario>> Listar();

        Task<TipoUsuario> BuscarPorID(int id);

        Task<TipoUsuario> Salvar(Presenca presenca);

        Task<TipoUsuario> Alterar(Presenca presenca);

        Task<TipoUsuario> Excluir(Presenca presenca);

        
    }
}