using FinancialManagementSystem.Core;

namespace FinancialManagementSystem.UI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            // Session must be enabled (app.UseSession() earlier)
            var token = context.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                // Set the header so JwtBearer middleware will pick it up
                context.Request.Headers["Authorization"] = "Bearer " + token;
            }
            await _next(context);
        }
    }
}