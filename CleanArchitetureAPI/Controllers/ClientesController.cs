using CleanArchiteture.Application.DTOs.Cliente;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitetureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IClienteParse _parse;

        public ClientesController(IClienteService service, IClienteParse parse)
        {
            _service = service;
            _parse = parse;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ClienteDto>> GetById(long id, CancellationToken cancellationToken)
        {
            // 1) Busca a entidade pelo Id
            Cliente cliente = await _service.Perquisar(id, cancellationToken);

            // 2) Se nao existir, retorna 404 Not Found
            if (cliente == null)
            {
                return NotFound();
            }

            // 3) Converte a entidade para o DTO de saida
            ClienteDto dto = _parse.Parse(cliente);

            // 4) Retorna 200 OK com o DTO
            // Versao compacta: return Ok(_parse.Parse(cliente));
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> GetAll(CancellationToken cancellationToken)
        {
            // 1) Busca todas as entidades
            IEnumerable<Cliente> clientes = await _service.Buscatodos(cancellationToken);

            // 2) Converte a lista de entidades para lista de DTOs
            List<ClienteDto> dtos = _parse.ParseList(clientes);

            // 3) Retorna 200 OK com a lista
            // Versao compacta: return Ok(_parse.ParseList(clientes));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Add([FromBody] CriarClienteDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de entrada -> entidade de dominio
            Cliente novoCliente = _parse.Parse(dto);

            // 2) Cria no banco (a entidade recebe o Id gerado automaticamente)
            Cliente clienteCriado = await _service.Criar(novoCliente, cancellationToken);

            // 3) Guarda o Id gerado pelo banco
            long idGerado = clienteCriado.Id;

            // 4) Entidade criada -> DTO de saida (corpo da resposta)
            ClienteDto dtoResposta = _parse.Parse(clienteCriado);

            // 5) Valores de rota para montar a URL do GET por Id (header Location)
            var valoresDeRota = new { id = idGerado };

            // 6) Monta a resposta 201 Created (status + Location + body)
            // Versao compacta: return CreatedAtAction(nameof(GetById), new { id = clienteCriado.Id }, _parse.Parse(clienteCriado));
            return CreatedAtAction(nameof(GetById), valoresDeRota, dtoResposta);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] AtualizarClienteDto dto, CancellationToken cancellationToken)
        {
            // 1) DTO de atualizacao -> entidade, com o Id vindo da rota
            Cliente cliente = _parse.Parse(dto, id);

            // 2) Tenta alterar; se nao existir, o service lanca KeyNotFoundException
            try
            {
                await _service.Alterar(cliente, cancellationToken);
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
