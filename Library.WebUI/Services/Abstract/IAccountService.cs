﻿using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IAccountService
    {
        Task<DataResult<Account>> GetByEmail(string name);
        Task<Result> Update(Account account);
    }
}