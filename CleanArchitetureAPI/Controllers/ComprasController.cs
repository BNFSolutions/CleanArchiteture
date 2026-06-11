using CleanArchiteture.Application.DTOs.Compra;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitetureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _service;
        private readonly ICompraParse _parse;

        public ComprasController(ICompraService service, ICompraParse parse)
        {
            _service = service;
            _parse = parse;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<CompraDto>> GetById(long id, CancellationToken cancellationToken)
        {
            // 1) Busca a entidade pelo Id
            Compra compra = await _service.Perquisar(id, cancellationToken);

            // 2) Se nao existir, retorna 404 Not Found
            if (compra == null)
            {
                return NotFound();
            }

            // 3) Converte a entidade para o DTO de saida
            CompraDto dto = _parse.Parse(compra);

            // 4) Retorna 200 OK com o DTO
            // Versao compacta: return Ok(_parse.Parse(compra));
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<CompraDto>>> GetAll(CancellationToken cancellationToken)
        {
            // 1) Busca todas as entidades
            IEnumerable<Compra> compras = await _service.Buscatodos(cancellationToken);

            // 2) Converte a lista de entidades para lista de DTOs
            List<CompraDto> dtos = _parse.ParseList(compras);

            // 3) Retorna 200 OK com a lista
            // Versao compacta: return Ok(_parse.ParseList(compras));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CompraDto>> Add([FromBody] CriarCompraDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de entrada -> entidade de dominio
            Compra novaCompra = _parse.Parse(dto);

            // 2) Cria no banco (a entidade recebe o Id gerado automaticamente)
            Compra compraCriada = await _service.Criar(novaCompra, cancellationToken);

            // 3) Guarda o Id gerado pelo banco
            long idGerado = compraCriada.Id;

            // 4) Entidade criada -> DTO de saida (corpo da resposta)
            CompraDto dtoResposta = _parse.Parse(compraCriada);

            // 5) Valores de rota para montar a URL do GET por Id (header Location)
            var valoresDeRota = new { id = idGerado };

            // 6) Monta a resposta 201 Created (status + Location + body)
            // Versao compacta: return CreatedAtAction(nameof(GetById), new { id = compraCriada.Id }, _parse.Parse(compraCriada));
            return CreatedAtAction(nameof(GetById), valoresDeRota, dtoResposta);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] AtualizarCompraDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de atualizacao -> entidade, com o Id vindo da rota
            Compra compra = _parse.Parse(dto, id);

            // 2) Tenta alterar; se nao existir, o service lanca KeyNotFoundException
            try
            {
                await _service.Alterar(compra, cancellationToken);
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
