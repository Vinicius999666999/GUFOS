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
    public class UsuarioController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>>Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var usuario = await _contexto.Usuario.ToListAsync();

            if (usuario == null){
                return NotFound();
            }

            return usuario ;
        }

        // GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var usuario = await _contexto.Evento.FindAsync(id);

            if (usuario == null){
                return NotFound();
            }

            return usuario;
        }

        // POST api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento usuario){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id,Usuario usuario){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != usuario.UsuarioId){
                return BadRequest();
            }

            // comparamos os atributos que foram mdeficados através do EF
            _contexto.Entry(usuario).State = EntityState . Modified;


            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _contexto.Evento.FindAsync(id);

                if(usuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/usuario/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<Evento>> Delete(int id){

            var usuario = await _contexto.Evento.FindAsync(id);
            if(usuario == null){
                return NotFound();
            }

            _contexto.Evento.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

    }
}