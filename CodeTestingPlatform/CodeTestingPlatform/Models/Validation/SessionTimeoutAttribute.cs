/*
https://www.nullplex.com/check-session-timeout-by-using-actionfilters-in-mvc/ 
*/
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeTestingPlatform.Models.Validation {
    public class SessionTimeoutAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            IHttpContextAccessor hc = new HttpContextAccessor();
            HttpContext ctx = hc.HttpContext;
            if (ctx.Session.GetString("IsAuthorized") == null) { // Doesn't care about True or False, just null
                ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                filterContext.Result = new RedirectToActionResult("Logout","Logout", new { isSessionExpired = true});
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
