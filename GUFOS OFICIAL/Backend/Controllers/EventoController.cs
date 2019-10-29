using Microsoft.AspNetCore.Mvc;
using Backend.Domains;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.Repositories;

//para adicionar a árvore de objeto adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace Backend.Controllers
{

    //Definimos nossa roda do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        
        //GufosContext _contexto = new GufosContext();

        EventoRepository _contexto = new EventoRepository();

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var eventos = await _contexto.Listar();

            if (eventos == null){
                return NotFound();
            }

            return eventos;
        }

        // GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id){

            //  FindAsync = procura algo específico no banco 
            Evento evento = await _contexto.BuscarPorID(id);

            if (evento == null){
                return NotFound();
            }

            return evento;
        }

        // POST api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento){

            try{
              
                await _contexto.Salvar(evento);

            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return evento;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Evento evento){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != evento.EventoId){
                return BadRequest();
            }

            try{
                await _contexto.Alterar(evento);
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var evento_valido = await _contexto.BuscarPorID(id);

                if(evento_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/evento/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<Evento>> Delete(int id){

            var evento = await _contexto.BuscarPorID(id);
            if(evento == null){
                return NotFound();
            }

            await _contexto.Excluir(evento);

            return evento;
        }

    }
}