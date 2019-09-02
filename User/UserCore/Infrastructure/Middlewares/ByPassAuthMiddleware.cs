using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCore.Infrastructure.Middlewares
{
    public class ByPassAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ByPassAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.Add("sign", "aaaaaaaaaaa");
            await _next.Invoke(context);
        }

    }
}
