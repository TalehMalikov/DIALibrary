using Library.Core.Domain.Enums;
using Library.DataAccess.Abstraction;
using Library.DataAccess.Implementation.PostgreSql;

namespace Library.DataAccess.Factories
{
    public static class DbFactory
    {
        private static IUnitOfWork? _unitOfWork;
        public static IUnitOfWork Create(ServerType type, string connectionString)
        {
            switch (type)
            {
                case ServerType.Postgre:
                    return _unitOfWork ??= new SqlUnitOfWork(connectionString); 
                case ServerType.MsSql:
                case ServerType.MySql:
                case ServerType.Oracle:
                default:
                    throw new NotSupportedException("We just support postgre!!");
            }
        }
    }
}
