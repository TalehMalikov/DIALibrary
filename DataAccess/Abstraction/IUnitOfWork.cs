namespace Library.DataAccess.Abstraction
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        global::Library.DataAccess.Abstraction.IAccountRoleRepository AccountRoleRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        IGroupRepository GroupRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IPublicationRepository PublicationRepository { get; }
        ISectorRepository SectionRepository { get; }
        ISpecialtyRepository SpecialtyRepository { get; }
        IStudentRepository StudentRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
