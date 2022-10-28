using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class AccountRoleDto : BaseDto
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }
    }
}
