using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegistrasiVM
    {
        public enum Gender { Male, Female }

        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public int Salary  { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int UniversityId { get; set; }
        public int EducationId { get; set; }



    }
}
