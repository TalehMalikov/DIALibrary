using Library.Core.DataAccess.Abstraction;
using Library.Core.DataAccess.Implementation.PostgreServer;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlUnitOfWork : BaseRepository, IUnitOfWork
    {
        public SqlUnitOfWork(string connectionString) : base(connectionString)
        {
        }

        public IAccountRepository AccountRepository => new SqlAccountRepository(connectionString);

        public IAccountRoleRepository AccountRoleRepository => new SqlAccountRoleRepository(connectionString);

        public IAuthorRepository AuthorRepository => new SqlAuthorRepository(connectionString);

        public IBookAuthorRepository BookAuthorRepository => new SqlBookAuthorRepository(connectionString);

        public IBookRepository BookRepository => new SqlBookRepository(connectionString);

        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(connectionString);

        public IFacultyRepository FacultyRepository => new SqlFacultyRepository(connectionString);

        public IGroupRepository GroupRepository => new SqlGroupRepository(connectionString);

        public ILanguageRepository LanguageRepository => new SqlLanguageRepository(connectionString);

        public IPublicationRepository PublicationRepository => new SqlPublicationRepository(connectionString);

        public ISectorRepository SectionRepository => new SqlSectorRepository(connectionString);

        public ISpecialtyRepository SpecialtyRepository => new SqlSpecialtyRepository(connectionString);

        public IStudentRepository StudentRepository => new SqlStudentRepository(connectionString);

        public IUserRepository UserRepository => new SqlUserRepository(connectionString);
    }
}
