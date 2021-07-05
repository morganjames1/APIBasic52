using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginRepository repository;

        public LoginController(LoginRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwToken = await repository.Auth(login);
            if (jwToken == null)
            {
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("JWToken", jwToken.Token);
            return RedirectToAction("Dashboard", "Admin");
        }
    }
}