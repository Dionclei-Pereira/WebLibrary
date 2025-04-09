using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services.Util {
    public class FilterMiddleware {

        private readonly ITokenService _tokenService;
        private readonly RequestDelegate _next;

        public FilterMiddleware(ITokenService tokenService, RequestDelegate next) {
            _tokenService = tokenService;
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            string path = context.Request.Path;
            Console.WriteLine(path);
            if (path.StartsWith("/api/auth", StringComparison.OrdinalIgnoreCase)) {
                await _next(context);
                return;
            }
            string token = context.Request.Headers["Authorization"].FirstOrDefault();
            TokenResult result = _tokenService.ValidateToken(token);

            if (result == null || !result.IsValid) {
                context.Response.StatusCode = 403;
                context.Response.WriteAsync("Forbidden");
                return;
            }

            context.Items["UserEmail"] = result.Email;
            await _next(context);
        }
    }
}
