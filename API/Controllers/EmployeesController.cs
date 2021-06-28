using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AllowOrigin")]

    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.repository = employeeRepository;

        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public  ActionResult Register(RegistrasiVM registrasiVM)
        {

            var insert = repository.Register(registrasiVM);
            if (insert == 2) 
            {
                return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Register Succes" });
            }
            else if (insert == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Failed Regsiter (Email Sudah digunakan)" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Failed Register (Nik Sudah digunakan)" });
            }
          
        }



        [HttpGet("RegistrasiView/{nik}")]

        public ActionResult RegistrasiViewByNik(string nik)
        {

            var get = repository.RegistrasiViewByNik(nik);
            if (get != null)
            {
                var gett = Ok(new { status = HttpStatusCode.OK, result = get, message = "Success" });
                return gett;
            }
            else
            {
                var gett = NotFound(new { status = HttpStatusCode.NotFound, result = get, message = "Not Found" });
                return gett;
            }
        }

      
        [HttpGet("RegistrasiView")]

        public ActionResult RegistrasiView()
        {

            var get = repository.RegistrasiView();
            if (get != null)
            {
                var gett = Ok(new { status = HttpStatusCode.OK, result = get, message = "Success" });
                return gett;
            }
            else
            {
                var gett = NotFound(new { status = HttpStatusCode.NotFound, result = get, message = "Not Found" });
                return gett;
            }


        }

    }
}
