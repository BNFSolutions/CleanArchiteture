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
                    // Constrói um dicionário campo -> mensagens (semelhante ao ModelState)
                    var errors = new Dictionary<string, string[]>();

                    // 1) Se o ValidationException contém um ValidationResult, usa-o para mapear membros (campos)
                    if (ex.ValidationResult != null)
                    {
                        var message = ex.ValidationResult.ErrorMessage ?? ex.Message;
                        var members = ex.ValidationResult.MemberNames?.ToList();

                        if (members == null || !members.Any())
                        {
                            // Sem members: mensagens gerais (chave vazia)
                            errors[string.Empty] = new[] { message };
                        }
                        else
                        {
                            // Para cada membro associado à ValidationResult, agrupa a mensagem
                            foreach (var member in members)
                            {
                                var key = string.IsNullOrWhiteSpace(member) ? string.Empty : member;
                                if (!errors.ContainsKey(key))
                                    errors[key] = new[] { message };
                                else
                                {
                                    var list = errors[key].ToList();
                                    list.Add(message);
                                    errors[key] = list.ToArray();
                                }
                            }
                        }
                    }

                    /* 2) Tratamento detalhado por coleção de ValidationResult (REMOVIDO/COMENTADO)
                       - Este bloco permitia que quem lançasse a exceção colocasse um IEnumerable<ValidationResult>
                         em ex.Data["ValidationResults"] para retornar múltiplos erros por campo.
                       - Comentado para simplificar o handler conforme solicitado.
                       - Para reativar:
                         * Remova este comentário e descomente o bloco abaixo.
                         * Garanta que o código que lança a ValidationException popule ex.Data["ValidationResults"]
                           com IEnumerable<ValidationResult>.
                    else if (ex.Data.Contains("ValidationResults") && ex.Data["ValidationResults"] is IEnumerable<ValidationResult> vrList)
                    {
                        foreach (var vr in vrList)
                        {
                            var msg = vr.ErrorMessage ?? string.Empty;
                            var members = vr.MemberNames?.ToList();

                            if (members == null || !members.Any())
                            {
                                if (!errors.ContainsKey(string.Empty)) errors[string.Empty] = new[] { msg };
                                else
                                {
                                    var list = errors[string.Empty].ToList();
                                    list.Add(msg);
                                    errors[string.Empty] = list.ToArray();
                                }

                                continue;
                            }

                            foreach (var member in members)
                            {
                                var key = string.IsNullOrWhiteSpace(member) ? string.Empty : member;
                                if (!errors.ContainsKey(key)) errors[key] = new[] { msg };
                                else
                                {
                                    var list = errors[key].ToList();
                                    list.Add(msg);
                                    errors[key] = list.ToArray();
                                }
                            }
                        }
                    }
                    */

                    // 3) Fallback: se não houver ValidationResult nem coleção, usa a mensagem simples
                    if (!errors.Any())
                    {
                        errors[string.Empty] = new[] { ex.Message };
                    }

                    // Monta ValidationProblemDetails para retorno consistente com ModelState
                    var validationDetails = new ValidationProblemDetails(errors)
                    {
                        Title = "Erros de validação",
                        Status = 400
                    };

                    context.Result = new BadRequestObjectResult(validationDetails);
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
