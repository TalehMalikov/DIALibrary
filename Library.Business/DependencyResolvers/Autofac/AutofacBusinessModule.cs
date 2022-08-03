using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Library.Business.Abstraction;
using Library.Business.Concrete;
using Library.Core.Interceptors;
using Library.DataAccess.Abstraction;
using Library.DataAccess.Implementation.PostgreSql;
using System.Reflection;
using Module = Autofac.Module;

namespace Library.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        private readonly string _connectionString;

        public AutofacBusinessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Business
            builder.RegisterType<AccountManager>().As<IAccountService>().SingleInstance();
            builder.RegisterType<AccountRoleManager>().As<IAccountRoleService>().SingleInstance();
            builder.RegisterType<AuthorManager>().As<IAuthorService>().SingleInstance();
            builder.RegisterType<BookAuthorManager>().As<IBookAuthorService>().SingleInstance();
            builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<FacultyManager>().As<IFacultyService>().SingleInstance();
            builder.RegisterType<GroupManager>().As<IGroupService>().SingleInstance();
            builder.RegisterType<LanguageManager>().As<ILanguageService>().SingleInstance();
            builder.RegisterType<PublicationManager>().As<IPublicationService>().SingleInstance();
            builder.RegisterType<SectorManager>().As<ISectorService>().SingleInstance();
            builder.RegisterType<SpecialtyManager>().As<ISpecialtyService>().SingleInstance();
            builder.RegisterType<StudentManager>().As<IStudentService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            #endregion

            #region DataAccess
            builder.Register(b => new BaseRepository()).AsSelf();
            builder.RegisterType<SqlAccountRepository>().As<IAccountRepository>().SingleInstance();
            builder.RegisterType<SqlAccountRoleRepository>().As<IAccountRoleRepository>().SingleInstance();
            builder.RegisterType<SqlAuthorRepository>().As<IAuthorRepository>().SingleInstance();
            builder.RegisterType<SqlBookAuthorRepository>().As<IBookAuthorRepository>().SingleInstance();
            builder.RegisterType<SqlBookRepository>().As<IBookRepository>().SingleInstance();
            builder.RegisterType<SqlCategoryRepository>().As<ICategoryRepository>().SingleInstance();
            builder.RegisterType<SqlFacultyRepository>().As<IFacultyRepository>().SingleInstance();
            builder.RegisterType<SqlGroupRepository>().As<IGroupRepository>().SingleInstance();
            builder.RegisterType<SqlLanguageRepository>().As<ILanguageRepository>().SingleInstance();
            builder.RegisterType<SqlPublicationRepository>().As<IPublicationRepository>().SingleInstance();
            builder.RegisterType<SqlSectorRepository>().As<ISectorRepository>().SingleInstance();
            builder.RegisterType<SqlSpecialtyRepository>().As<ISpecialtyRepository>().WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlStudentRepository>().As<IStudentRepository>().SingleInstance();
            builder.RegisterType<SqlUserRepository>().As<IUserRepository>().SingleInstance();
            #endregion

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
