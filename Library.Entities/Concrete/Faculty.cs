﻿using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Faculty : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}