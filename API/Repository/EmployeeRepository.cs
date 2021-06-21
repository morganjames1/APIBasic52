using API.Context;
using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;

        //Perintah dibawah ini agar fungsi crud bisa dijalankan
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int delete(string nik)
        {
          
            try
            {
                var find = myContext.Employees.Find(nik);
                myContext.Employees.Remove(find);
                var delete = myContext.SaveChanges();
                return delete;
            }
            catch (ArgumentNullException)
            {

                return 0;
            }
        }

        public IEnumerable<Employee> Get()
        {

            var employee = myContext.Employees.ToList();
            return employee;
        }

        // Tidak menggunakan save changes karna tidak merubah data apapun, hanya menampilkan
        // BIsa saja dibuat namun tidak efektif karna sama saja

        public Employee Get(string nik)
        {
            return myContext.Employees.Find(nik);
        }

        public int insert(Employee employee)
        {
            try
            {

                myContext.Employees.Add(employee);
                var insert = myContext.SaveChanges();
                return insert;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public int update(Employee employee, string nik)
        {

            try
            {
                var employess = myContext.Employees.Find(nik);
                //Employee employess = new Employee();
                if (employee.FirstName != null)
                    employess.FirstName = employee.FirstName;

                if (employee.LastName != null)
                    employess.LastName = employee.LastName;

                if (employee.Email != null)
                    employess.Email = employee.Email;

                if (employee.Salary != 0)
                    employess.Salary = employee.Salary;

                if (employee.PhoneNumber != null)
                    employess.PhoneNumber = employee.PhoneNumber;

                if (employee.BirthDate != null)
                    employess.BirthDate = employee.BirthDate;

                myContext.Entry(employess).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                return result;
            }         
            catch (NullReferenceException)
            {
                return 0;
            }
           
        }
    }
}
