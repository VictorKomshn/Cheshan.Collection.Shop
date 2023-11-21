using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Http;

namespace Cheshan.Collection.Shop.Middleware
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context, ICartsService carts)
        {
            var activeuser = context.Request.Cookies["ActiveUser"];
            if (activeuser == null)
            {
                context.Response.Redirect("");
            }

            await _next.Invoke(context);
        } 
    }
}
