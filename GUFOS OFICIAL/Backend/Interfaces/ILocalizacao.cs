using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface ILocalizacao
    {
        
        Task<List<Localizacao>> Listar();

        Task<Localizacao> BuscarPorID(int id);

        Task<Localizacao> Salvar(Categoria categoria);

        Task<Localizacao> Alterar(Evento evento);

        Task<Localizacao> Excluir(Evento evento);
    }
}