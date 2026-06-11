using System.Collections.Generic;

namespace CleanArchiteture.Application.Parsing.Interface
{
    /// <summary>
    /// Contrato generico de conversao bidirecional entre uma origem e um destino,
    /// para um item (Parse) e para listas (ParseList).
    /// </summary>
    public interface IParse<TSource, TDestination>
    {
        TDestination Parse(TSource origin);
        TSource Parse(TDestination origin);
        List<TDestination> ParseList(IEnumerable<TSource> origin);
        List<TSource> ParseList(IEnumerable<TDestination> origin);
    }
}
