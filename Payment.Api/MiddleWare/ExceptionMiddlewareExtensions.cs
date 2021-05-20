using Microsoft.AspNetCore.Builder;

namespace Payment.Api.MiddleWare
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UsePaymentExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<PaymentExceptionMiddleware>();
        }
    }

}
