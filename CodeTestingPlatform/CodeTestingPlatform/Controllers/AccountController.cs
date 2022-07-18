using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Controllers {
    public class AccountController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult AccessDenied() {
            return View();
        }
    }
}
