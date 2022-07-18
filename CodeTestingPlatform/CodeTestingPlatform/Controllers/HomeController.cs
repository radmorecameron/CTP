using CodeTestingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Help() {
            return View();
        }

        public IActionResult About() {
            return View();
        }

        public IActionResult ReleaseNotes() {
            return View();
        }

        //  is needed. Otherwise, it may break the Swashbuckle swagger
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            //string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            //var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //if (feature != null) {
            //    if (feature.Error is FileNotFoundException) {
            //        // to-do
            //    }
            //    _logger.LogError(feature.Error, feature.Path);
            //}
            //return View();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Status(int code) {
            //string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            //var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            //string originalURL = null;
            //if (statusCodeReExecuteFeature != null) {
            //    originalURL =
            //        statusCodeReExecuteFeature.OriginalPathBase
            //        + statusCodeReExecuteFeature.OriginalPath
            //        + statusCodeReExecuteFeature.OriginalQueryString;
            //}

            var defaultStatus = new ResponseStatus {
                Code = code,
                Title = "The resource you are accessing is unavailable.",
                Description = "The server  was unable to complete your request.."
            };

            var status = RESPONSE_STATUS_LIST.SingleOrDefault(s => s.Code == code);

            if (null == status)
                status = defaultStatus;

            return View(status);
        }

        [HttpPost]
        public IActionResult SelectLanguage(string culture, string returnUrl) {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        readonly ResponseStatus[] RESPONSE_STATUS_LIST = new ResponseStatus[]
        {   new ResponseStatus
            {
                Code = 400,
                Title = "Bad Request",
                Description = "The request you made cannot be processed. The problem could be malformed syntax, invalid formatting, or deceptive request routing."
            },
            new ResponseStatus
            {
                Code = 401,
                Title = "Unauthorized",
                Description = "You must authenticate itself to get the requested response."
            },
            new ResponseStatus
            {
                Code = 403,
                Title = "Forbidden",
                Description = "You does not have access rights to the content."
            },
            new ResponseStatus
            {
                Code = 404,
                Title = "Page not found",
                Description = "The resource you are looking for might have been removed, had its name changed, or is temporarily unavailable."
            },
            new ResponseStatus
            {
                Code = 415,
                Title = "Unsupported Media Type",
                Description = "The server does not handled requests for media in the type specified in the request."
            },
            new ResponseStatus
            {
                Code = 416,
                Title = "Requested Range Not Satisfiable",
                Description = "The request you made cannot be completed, likely because the requested record is not in the data."
            },
        };
    }
}
