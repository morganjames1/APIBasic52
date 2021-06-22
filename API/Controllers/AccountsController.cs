using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.repository = accountRepository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var response = repository.Login(loginVM);
            if (response == 2)
            {
                var pos = Ok(new { status = HttpStatusCode.OK, result = response, message = "Login Successfull" });
                return pos;
            }
            else if (response == 1)
            {
                var pos = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Password Tidak Ditemukan" });
                return pos;
            }
            else
            {
                var pos = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Nik Tidak Ditemukan" });
                return pos;
            }
        }
    }
}
