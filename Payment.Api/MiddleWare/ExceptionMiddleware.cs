using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Payment.Api.MiddleWare
{
    public class PaymentExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PaymentExceptionMiddleware> _logger;

        public PaymentExceptionMiddleware(RequestDelegate next, ILogger<PaymentExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentException e)
            {
                await HandleException(httpContext, e, HttpStatusCode.NotFound);
            }
            catch (ValidationException e)
            {
                await HandleException(httpContext, e, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, e, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception ex, HttpStatusCode statusCode)
        {
            _logger.LogError(ex, "");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
    }

}
