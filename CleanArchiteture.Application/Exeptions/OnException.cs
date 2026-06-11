using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace CleanArchiteture.Application.Exeptions
{
    public static class OnException
    {
        public static void Handle(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case KeyNotFoundException ex:
                    context.Result = new NotFoundObjectResult(ex.Message);
                    break;

                case ValidationException ex:
                    context.Result = new BadRequestObjectResult(ex.Message);
                    break;

                default:
                    context.Result = new ObjectResult("Erro interno")
                    {
                        StatusCode = 500
                    };
                    break;
            }
        }
    }
}
