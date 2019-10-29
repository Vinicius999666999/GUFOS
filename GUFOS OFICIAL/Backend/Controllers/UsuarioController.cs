using Microsoft.AspNetCore.Mvc;
using Backend.Domains;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Repositories;

//para adicionar a árvore de objeto adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace Backend.Controllers
{

    //Definimos nossa roda do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        
        //GufosContext _contexto = new GufosContext();

        UsuarioRepository _contexto = new UsuarioRepository();

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var usuario = await _contexto.Listar();

            if (usuario == null){
                return NotFound();
            }

            return usuario ;
        }

        // GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id){

            //  FindAsync = procura algo específico no banco 
            var usuario = await _contexto.BuscarPorID(id);

            if (usuario == null){
                return NotFound();
            }

            return usuario;
        }

        // POST api/Evento
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){

            try{

                // Tratamos contra ataques de SQL Injection
                await _contexto.Salvar(usuario);

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

            try{

                await _contexto.Alterar(usuario);

            }
            catch(DbUpdateConcurrencyException)
            {

                //Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _contexto.BuscarPorID(id);

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

        public async Task<ActionResult<Usuario>> Delete(int id){

            var usuario = await _contexto.BuscarPorID(id);
            if(usuario == null){
                return NotFound();
            }

            await _contexto.Excluir(usuario);

            return usuario;
        }

    }
}