using API.Models;
using API.Repository;
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
    public class EmployeesControllerLama : ControllerBase
    {
        private EmployeeRepository employeeRepository;


        public EmployeesControllerLama(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var get = employeeRepository.Get();
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

        //[HttpGet("/API/Employees/{nik}")]
        [HttpGet("{nik}")]
        public ActionResult Get(string nik)
        {
            var get = employeeRepository.Get(nik);
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

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var insert = employeeRepository.insert(employee);
            if (insert > 0) // Jika result lebih dari 1
            {
                return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Insert Succes" });
            }
            else
            {
                return BadRequest("Fail to insert");
            }
        }

        [HttpDelete]
        public ActionResult Delete(string nik)
        {
            var delete = employeeRepository.delete(nik);
            if (delete > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = delete, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = delete, message = "Gagal Delete" });
            }  

        }
    

        [HttpPut]
        public ActionResult Update (Employee employee, string nik)
        {
            var response = employeeRepository.update(employee, nik);

            {
                if (response > 0)
                {                   
                    return Ok(new { status = HttpStatusCode.OK, result = Ok().StatusCode, message = "Update Succesfull" });
                }
                else
                {                  
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = BadRequest().StatusCode, message = "Fail Update" });
                }
            }
        }
    }
}
