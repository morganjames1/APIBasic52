using API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository <Entity, Key>
    {

        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public ActionResult Get()
        {
            var get = repository.Get();
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


        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var get = repository.Get(key);
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
        public ActionResult Post(Entity entity)
        {
            var insert = repository.insert(entity);
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
        public ActionResult Delete(Key key)
        {
            var delete = repository.delete(key);
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
        public ActionResult Update(Entity entity, Key key)
        {
            var response = repository.update(entity, key);
            if (response > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Berhasil Update" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Update Gagal" });
            }
        }


    }
}
