using Microsoft.AspNetCore.Identity;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services.Util {
    public class FilterMiddleware {

        private readonly IServiceProvider _serviceProvider;
        private readonly RequestDelegate _next;

        public FilterMiddleware(IServiceProvider serviceProvider, RequestDelegate next) {
            _serviceProvider = serviceProvider;
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            string path = context.Request.Path;
            var scope = _serviceProvider.CreateScope();
            var _tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            Console.WriteLine(path);
            if (path.StartsWith("/api/auth", StringComparison.OrdinalIgnoreCase)) {
                await _next(context);
                return;
            }
            string token = context.Request.Headers["Authorization"].FirstOrDefault();
            TokenResult result = await _tokenService.ValidateToken(token);

            if (result == null || !result.IsValid) {
                context.Response.StatusCode = 403;
                context.Response.WriteAsync("Forbidden");
                return;
            }
            context.Items["Role"] = result.Role;
            context.Items["UserEmail"] = result.Email;
            await _next(context);
        }
    }
}
