﻿using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class AccountViewModel
    {
        public AccountDto AccountDto { get; set; }
        public StudentDto StudentDto { get; set; }
        public List<Account> Accounts { get; set; }
        public SelectList Roles { get; set; }
        public AccountRole AccountRole { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public SelectList SpecialtyList { get; set; }
        public  SelectList GroupList { get; set; }
    }
}
