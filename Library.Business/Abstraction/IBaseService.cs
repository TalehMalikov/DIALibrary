﻿using Library.Core.Result.Concrete;

namespace Library.Business.Abstraction
{
    public interface IBaseService<T>
    {
        Result Add(T value);
        Result Update(T value);
        Result Delete(T value);
        DataResult<T> Get(int id);
        DataResult<List<T>> GetAll();
    }
}