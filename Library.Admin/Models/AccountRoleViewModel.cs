using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class AccountRoleViewModel
    {
        public Role Role { get; set; }
        public List<Role> Roles { get; set; }
        public List<AccountRole> AccountRoles { get; set; }
        public AccountRole AccountRole { get; set; }
        public SelectList RoleList { get; set; }
        public SelectList AccountList { get; set; }
    }
}
