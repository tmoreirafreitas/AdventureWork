using AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;

namespace AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            string result;
            int code;

            if (exception is NotFoundException notFoundException)
            {
                code = (int)HttpStatusCode.NotFound;
                result = JsonConvert.SerializeObject(new
                {
                    code,
                    message = notFoundException.Message
                });
            }
            else if (exception is UserFriendlyException friendlyException)
            {
                code = (int)HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new
                {
                    code,
                    message = exception.Message

                });
                _logger.LogError(exception, "Exceção não tratada.", null);
            }
            else if (exception is SqlException sqlException)
            {
                code = (int)HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new
                {
                    code,
                    message = exception.Message.Split('\r')[0].Trim()

                });
                _logger.LogError(exception, "Exceção de SQL.", null);
            }
            else
            {
                code = (int)HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new
                {
                    code,
                    title = "Oops!",
                    message = "Encontramos uma falha ao tentar realizar esta operação no momento."
                });

                _logger.LogError(exception, "Exceção não tratada.", null);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}
