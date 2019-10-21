using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

//para adicionar a árvore de objeto adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace Backend.Controllers
{

    //Definimos nossa roda do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Evento>>>Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var eventos = await _contexto.Evento.ToListAsync();

            if (eventos == null){
                return NotFound();
            }

            return eventos;
        }

        // GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var evento = await _contexto.Evento.FindAsync(id);

            if (evento == null){
                return NotFound();
            }

            return evento;
        }

        // POST api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(evento);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
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

            // comparamos os atributos que foram mdeficados através do EF
            _contexto.Entry(evento).State = EntityState . Modified;


            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var evento_valido = await _contexto.Evento.FindAsync(id);

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

            var evento = await _contexto.Evento.FindAsync(id);
            if(evento == null){
                return NotFound();
            }

            _contexto.Evento.Remove(evento);
            await _contexto.SaveChangesAsync();

            return evento;
        }

    }
}