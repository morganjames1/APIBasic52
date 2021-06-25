﻿using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{

    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }


        public static bool ValidatePassword(string password, string corectHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, corectHash);
        }



        public int Login(LoginVM loginVM)
        {
            var login = myContext.Employees.Where(x => (x.NIK == loginVM.NIK) || (x.Email == loginVM.Email)).FirstOrDefault<Employee>();
            if (login != null)
            {
                var validate = ValidatePassword(loginVM.Password, login.Account.Password);
                if (validate)
                {
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


            public int ResetPassword(LoginVM reset)

            {


            var account = new Account();
            Guid guid = Guid.NewGuid();
            string emailG = guid.ToString();

            var email = myContext.Employees.FirstOrDefault(a => a.Email == reset.Email);
            if (email != null)
            {
                account.NIK = email.NIK;
                account.Password = emailG;
                myContext.Entry(account).State = EntityState.Modified;
                var insert = myContext.SaveChanges();

                if (insert > 0)
                {
                    var getEmail = reset.Email;
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);

                    mail.From = new MailAddress("moremore13arn@gmail.com");
                    mail.To.Add(getEmail);
                    mail.Subject = "Reset Password";
                    mail.Body = "Hallo " + email.FirstName + System.Environment.NewLine + "This is your password : " + emailG;

                    smtpServer.UseDefaultCredentials = true;
                    smtpServer.Credentials = new NetworkCredential("moremore13arn@gmail.com", "hitamputih1");
                    smtpServer.EnableSsl = true;
                    smtpServer.Send(mail);

                    var find1 = myContext.Employees.Where(e => e.Email == getEmail).FirstOrDefault<Employee>();
                    var find2 = myContext.Accounts.Find(find1.NIK);

                    find2.Password = BCrypt.Net.BCrypt.HashPassword(guid.ToString(), GetRandomSalt());
                    myContext.SaveChanges();
                } return insert;
            }
            else
            {
                return 0;
            }
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var login = myContext.Employees.Where(x => (x.NIK == changePasswordVM.NIK) || (x.Email == changePasswordVM.Email)).FirstOrDefault<Employee>();
            if (login != null)
            {
                
                var checkPass = BCrypt.Net.BCrypt.Verify(changePasswordVM.OldPassword, login.Account.Password);

                if (checkPass)
                {
                    var changePass = myContext.Accounts.Find(login.NIK);
                    changePass.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword, GetRandomSalt());
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
