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
    public class TipoUsuarioController : ControllerBase
    {
        
        GufosContext _contexto = new GufosContext();

        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>>Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var tipousuario = await _contexto.TipoUsuario.ToListAsync();

            if (tipousuario == null){
                return NotFound();
            }

            return tipousuario;
        }

        // GET: api/TipoUsuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);

            if (tipousuario == null){
                return NotFound();
            }

            return tipousuario;
        }

        // POST api/TipoUsuario
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipousuario){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(tipousuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return tipousuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, TipoUsuario tipousuario){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != tipousuario.TipoUsuarioId){
                return BadRequest();
            }

            // comparamos os atributos que foram mdeficados através do EF
            _contexto.Entry(tipousuario).State = EntityState . Modified;


            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var tipousuario_valido = await _contexto.TipoUsuario.FindAsync(id);

                if(tipousuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/tipousuario/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<TipoUsuario>> Delete(int id){

            var tipousuario = await _contexto.TipoUsuario.FindAsync(id);
            if(tipousuario == null){
                return NotFound();
            }

            _contexto.TipoUsuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();

            return tipousuario;
        }

    }
}