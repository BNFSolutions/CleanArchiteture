using System.Collections.Generic;
using CleanArchiteture.Application.DTOs.Categoria;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing
{
    public class CategoriaParse : ICategoriaParse
    {
        public Categoria Parse(CategoriaDto origin)
        {
            if (origin == null) return null;

            var destino = new Categoria
            {
                Id = origin.IdDto,
                CodCategoria = origin.CodCategoriaDto,
                DescCategoria = origin.DescCategoriaDto
            };

            return destino;
        }

        public CategoriaDto Parse(Categoria origin)
        {
            if (origin == null) return null;

            var destino = new CategoriaDto
            {
                IdDto = origin.Id,
                CodCategoriaDto = origin.CodCategoria,
                DescCategoriaDto = origin.DescCategoria
            };

            return destino;
        }

        public List<Categoria> ParseList(IEnumerable<CategoriaDto> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<Categoria>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public List<CategoriaDto> ParseList(IEnumerable<Categoria> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<CategoriaDto>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public Categoria Parse(CriarCategoriaDto origin)
        {
            if (origin == null) return null;

            var destino = new Categoria
            {
                CodCategoria = origin.CodCategoriaDto,
                DescCategoria = origin.DescCategoriaDto
            };

            return destino;
        }

        public Categoria Parse(AtualizarCategoriaDto origin, long id)
        {
            if (origin == null) return null;

            var destino = new Categoria
            {
                Id = id,
                CodCategoria = origin.CodCategoriaDto,
                DescCategoria = origin.DescCategoriaDto
            };

            return destino;
        }
    }
}
