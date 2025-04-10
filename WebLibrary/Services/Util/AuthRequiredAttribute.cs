using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebLibrary.Services.Util {
    public class AuthRequiredAttribute : Attribute, IAuthorizationFilter {

        private string role { get; set; }

        public AuthRequiredAttribute(string role) {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            var userRole = context.HttpContext.Items["Role"];
            if (userRole == null || role != userRole.ToString()) {
                context.Result = new ForbidResult();
            }
        }
    }
}
