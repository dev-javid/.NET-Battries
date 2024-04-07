using Microsoft.AspNetCore.Http;
using Serilog.Ui.Web;
using Serilog.Ui.Web.Authorization;

namespace $safeprojectname$.Filters
{
    internal class LogAuthorizeFilter : IUiAuthorizationFilter
    {
        public bool Authorize(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            if (path == "/serilog-ui/index.html" || httpContext.Request.IsLocal())
            {
                return true;
            }

            return false;
        }
    }
}
