using Library.Core.DataAccess.Abstraction;
using Library.Core.DataAccess.Implementation.PostgreSql;
using Library.Core.Domain.Enums;

namespace Library.Core.Factories
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
