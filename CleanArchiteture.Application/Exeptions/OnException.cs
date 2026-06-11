using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchiteture.Application.Exeptions
{
    public static class OnException
    {
        /// <summary>
        /// Trata exceções lançadas na aplicação e converte em respostas HTTP.
        /// - KeyNotFoundException => 404
        /// - ValidationException => 400 (ValidationProblemDetails)
        /// - Demais => 500
        /// </summary>
        public static void Handle(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case KeyNotFoundException ex:
                    // Entidade não encontrada -> 404 com mensagem simples
                    context.Result = new NotFoundObjectResult(ex.Message);
                    break;

                case ValidationException ex:
                    context.Result = new BadRequestObjectResult(ex.Message);
                    break;

                default:
                    // Erro genérico => 500
                    context.Result = new ObjectResult("Erro interno")
                    {
                        StatusCode = 500
                    };
                    break;
            }
        }
    }
}
