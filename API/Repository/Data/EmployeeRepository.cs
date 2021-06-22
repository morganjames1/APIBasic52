using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext; //Buat Objek null

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext; // Isi dari mycontext dari database
        }

        public int Register(RegistrasiVM registrasiVM)
   
        {

            Employee employee = new Employee();
            Account account = new Account();
            Education education = new Education();
            Profiling profiling = new Profiling();

            var check1 = myContext.Employees.Find(registrasiVM.NIK);
            if (check1 == null)
            {
                var check2 = myContext.Employees.Where(e => e.Email == registrasiVM.Email).FirstOrDefault<Employee>();
                if (check2 == null)
                {
                    
                        employee.NIK = registrasiVM.NIK;
                        employee.FirstName = registrasiVM.FirstName;
                        employee.LastName = registrasiVM.LastName;
                        employee.PhoneNumber = registrasiVM.PhoneNumber;
                        employee.BirthDate = registrasiVM.Birthdate;
                        employee.gender = (Employee.Gender)registrasiVM.gender;
                        employee.Salary = registrasiVM.Salary;
                        employee.Email = registrasiVM.Email;
                        myContext.Employees.Add(employee);
                        myContext.SaveChanges();

                        account.NIK = registrasiVM.NIK;
                        account.Password = registrasiVM.Password;
                        myContext.Accounts.Add(account);
                        myContext.SaveChanges();

                        education.Degree = registrasiVM.Degree;
                        education.GPA = registrasiVM.GPA;
                        education.UniversityId = registrasiVM.UniversityId;
                        myContext.Educations.Add(education);
                        myContext.SaveChanges();

                        profiling.NIK = registrasiVM.NIK;
                        profiling.EducationId = registrasiVM.EducationId;
                        myContext.Profilings.Add(profiling);
                        myContext.SaveChanges();

                        return 2;
                    }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
                       
                
        }

    }
   
}
