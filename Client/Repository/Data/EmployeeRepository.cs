using API.Models;
using Client.Base;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        internal Task RegistrasiView()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegistrasiVM>> GetRegistrasiView()
        {
            List<RegistrasiVM> entities = new List<RegistrasiVM>();

            using (var response = await httpClient.GetAsync(request + "RegistrasiView/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegistrasiVM>>(apiResponse);
            }
            return entities;
        }

    }
}
