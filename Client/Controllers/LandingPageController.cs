using API.Models;
using Client.Base;
using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LandingPageController : Controller
    {

        public IActionResult LP()
        {
            return View();
        }
    }
}