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
    public class CategoriaController : ControllerBase
    {
        
        // GufosContext _contexto = new GufosContext();

        CategoriaRepository _repository = new CategoriaRepository();

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get(){

            var categoria = await _repository.Listar();

            if (categoria == null){
                return NotFound();
            }

            return categoria;
        }

        // GET: api/Categoria/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id){

            //  FindAsync = procura algo específico no banco 
            var categoria = await _repository.BuscarPorID(id);

            if (categoria == null){
                return NotFound();
            }

            return categoria;
        }

        // POST api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria){

            try{

              await _repository.Salvar(categoria);

            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return categoria;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Alterar(categoria);
            }
            catch (DbUpdateConcurrencyException)
            {
                var categoria_valido = _repository.BuscarPorID(id);

                if (categoria_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE api/categoria/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _repository.BuscarPorID(id);
            if(categoria == null){
                return NotFound();
            }

            await _repository.Excluir(categoria); 

            return categoria;
        }

    }
}