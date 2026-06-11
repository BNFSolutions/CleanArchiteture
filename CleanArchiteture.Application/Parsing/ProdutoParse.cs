using System.Collections.Generic;
using CleanArchiteture.Application.DTOs.Produto;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing
{
    public class ProdutoParse : IProdutoParse
    {
        public Produtos Parse(ProdutoDto origin)
        {
            if (origin == null) return null;

            var destino = new Produtos
            {
                Id = origin.IdDto,
                CodProduto = origin.CodProdutoDto,
                DescProduto = origin.DescProdutoDto,
                VlrPreco = origin.VlrPrecoDto,
                DataCadastro = origin.DataCadastroDto,
                CategoriaId = origin.CategoriaIdDto
            };

            return destino;
        }

        public ProdutoDto Parse(Produtos origin)
        {
            if (origin == null) return null;

            var destino = new ProdutoDto
            {
                IdDto = origin.Id,
                CodProdutoDto = origin.CodProduto,
                DescProdutoDto = origin.DescProduto,
                VlrPrecoDto = origin.VlrPreco,
                DataCadastroDto = origin.DataCadastro,
                CategoriaIdDto = origin.CategoriaId
            };

            return destino;
        }

        public List<Produtos> ParseList(IEnumerable<ProdutoDto> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<Produtos>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public List<ProdutoDto> ParseList(IEnumerable<Produtos> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<ProdutoDto>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public Produtos Parse(CriarProdutoDto origin)
        {
            if (origin == null) return null;

            var destino = new Produtos
            {
                CodProduto = origin.CodProdutoDto,
                DescProduto = origin.DescProdutoDto,
                VlrPreco = origin.VlrPrecoDto,
                CategoriaId = origin.CategoriaIdDto
            };

            return destino;
        }

        public Produtos Parse(AtualizarProdutoDto origin, long id)
        {
            if (origin == null) return null;

            var destino = new Produtos
            {
                Id = id,
                CodProduto = origin.CodProdutoDto,
                DescProduto = origin.DescProdutoDto,
                VlrPreco = origin.VlrPrecoDto,
                CategoriaId = origin.CategoriaIdDto
            };

            return destino;
        }
    }
}
