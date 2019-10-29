using Microsoft.AspNetCore.Mvc;
using Backend.Domains;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.Repositories;

namespace Backend.Controllers
{

    //Definimos nossa roda do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        
        //GufosContext _contexto = new GufosContext();

        PresencaRepository _contexto = new PresencaRepository();

        // GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>>Get(){

            var presencas = await _contexto.Listar();

            if (presencas == null){
                return NotFound();
            }

            return presencas;
        }

        // GET: api/Presenca/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var presenca = await _contexto.BuscarPorID(id);

            if (presenca == null){
                return NotFound();
            }

            return presenca;
        }

        // POST api/Presenca
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.Salvar(presenca);
                // Salvamos efetivamente o nosso objeto no banco de dados
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return presenca;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Presenca presenca){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != presenca.PresencaId)
            {
                return BadRequest();
            }

            try
            {
                await _contexto.Alterar(presenca);
            }
            catch (DbUpdateConcurrencyException)
            {

                //Verificamos se o objeto inserido realmente existe no banco
                var presenca_valido = await _contexto.BuscarPorID(id);

                if(presenca_valido == null)
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/presenca/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<Presenca>> Delete(int id){

            var presenca = await _contexto.BuscarPorID(id);
            if(presenca == null){
                return NotFound();
            }

            await _contexto.Excluir(presenca);

            return presenca;
        }

    }
}