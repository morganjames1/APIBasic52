using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    interface IEmployeeRepository
    {
        //Untuk perulangan menggunakan IEnumerable<model> kemudian diikuti nama function yaitu get
        IEnumerable<Employee> Get();
        Employee Get(string nik);     
        int insert(Employee employee);
        int update(Employee employee, string nik);
        int delete(string nik);
        
    }
}
