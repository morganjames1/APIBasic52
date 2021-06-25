using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_T_Role")]
    public class Role
    {
        [Key]
        public string RoleId { get; set; }

        public  string RoleName { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
