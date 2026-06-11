using CleanArchiteture.Application.DTOs.Categoria;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitetureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;
        private readonly ICategoriaParse _parse;

        public CategoriasController(ICategoriaService service, ICategoriaParse parse)
        {
            _service = service;
            _parse = parse;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<CategoriaDto>> GetById(long id, CancellationToken cancellationToken)
        {
            // 1) Busca a entidade pelo Id
            Categoria categoria = await _service.Perquisar(id, cancellationToken);

            // 2) Se nao existir, retorna 404 Not Found
            if (categoria == null)
            {
                return NotFound();
            }

            // 3) Converte a entidade para o DTO de saida
            CategoriaDto dto = _parse.Parse(categoria);

            // 4) Retorna 200 OK com o DTO
            // Versao compacta: return Ok(_parse.Parse(categoria));
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDto>>> GetAll(CancellationToken cancellationToken)
        {
            // 1) Busca todas as entidades
            IEnumerable<Categoria> categorias = await _service.Buscatodos(cancellationToken);

            // 2) Converte a lista de entidades para lista de DTOs
            List<CategoriaDto> dtos = _parse.ParseList(categorias);

            // 3) Retorna 200 OK com a lista
            // Versao compacta: return Ok(_parse.ParseList(categorias));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Add([FromBody] CriarCategoriaDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de entrada -> entidade de dominio
            Categoria novaCategoria = _parse.Parse(dto);

            // 2) Cria no banco (a entidade recebe o Id gerado automaticamente)
            Categoria categoriaCriada = await _service.Criar(novaCategoria, cancellationToken);

            // 3) Guarda o Id gerado pelo banco
            long idGerado = categoriaCriada.Id;

            // 4) Entidade criada -> DTO de saida (corpo da resposta)
            CategoriaDto dtoResposta = _parse.Parse(categoriaCriada);

            // 5) Valores de rota para montar a URL do GET por Id (header Location)
            var valoresDeRota = new { id = idGerado };

            // 6) Monta a resposta 201 Created (status + Location + body)
            // Versao compacta: return CreatedAtAction(nameof(GetById), new { id = categoriaCriada.Id }, _parse.Parse(categoriaCriada));
            return CreatedAtAction(nameof(GetById), valoresDeRota, dtoResposta);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] AtualizarCategoriaDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de atualizacao -> entidade, com o Id vindo da rota
            Categoria categoria = _parse.Parse(dto, id);

            // 2) Tenta alterar; se nao existir, o service lanca KeyNotFoundException
            try
            {
                await _service.Alterar(categoria, cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            // 3) Sucesso, sem corpo de resposta (204 No Content)
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            // 1) Tenta excluir; se nao existir, o service lanca KeyNotFoundException
            try
            {
                await _service.Deletar(id, cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            // 2) Sucesso, sem corpo de resposta (204 No Content)
            return NoContent();
        }

        [HttpGet("{id:long}/exists")]
        public async Task<ActionResult<bool>> Exists(long id, CancellationToken cancellationToken)
        {
            // 1) Pergunta ao service se o Id existe
            bool existe = await _service.Existe(id, cancellationToken);

            // 2) Retorna 200 OK com true/false
            return Ok(existe);
        }
    }
}
