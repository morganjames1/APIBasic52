using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{

    //[Authorize]
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
                var pos = Ok(new JWTokenVM { Status = HttpStatusCode.OK, Token = repository.GenerateTokenLogin(loginVM), Message = "Login Successfull" });
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


        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(LoginVM reset)
        {
            var response = repository.ResetPassword(reset); 
            if (response == 1)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = response, messsage = "Email Berhasil Dikirim" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, messsage = "Gagal Mengirim Email" });
            }
        }


        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var response = repository.ChangePassword(changePasswordVM);
            if (response == 2)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = response, message = "Password Berhasil Diganti" });
            }
            else if (response == 1)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "Password Tidak Sesuai"});
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "Nik / Password Tidak Ditemukan"});
            }
        }







    }
}
