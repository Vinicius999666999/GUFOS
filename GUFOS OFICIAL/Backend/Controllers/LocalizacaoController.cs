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
    public class LocalizacaoController : ControllerBase
    {
        
        LocalizacaoRepository _contexto = new LocalizacaoRepository();

        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get(){

            var localizacaos = await _contexto.Listar();

            if (localizacaos == null){
                return NotFound();
            }

            return localizacaos;
        }

        // GET: api/Localizacao/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var localizacao = await _contexto.BuscarPorID(id);

            if (localizacao == null){
                return NotFound();
            }

            return localizacao;
        }

        // POST api/Localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.Salvar(localizacao);
                // Salvamos efetivamente o nosso objeto no banco de dados
                
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return localizacao;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Localizacao localizacao){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != localizacao.LocalizacaoId){
                return BadRequest();
            }

            try
            {
                await _contexto.Alterar(localizacao);
            }
            catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var localizacao_valido = _contexto.BuscarPorID(id);

                if(localizacao_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/localizacao/id
        [HttpDelete("{id}")]

        public async Task<ActionResult<Localizacao>> Delete(int id){

            var localizacao = await _contexto.BuscarPorID(id);
            if(localizacao == null){
                return NotFound();
            }

            await  _contexto.Excluir(localizacao);

            return localizacao;
        }

    }
}