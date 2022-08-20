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
            builder.RegisterType<ActivityManager>().As<IActivityService>().SingleInstance();
            #endregion

            #region DataAccess
            builder.RegisterType<SqlAccountRepository>().As<IAccountRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlAccountRoleRepository>().As<IAccountRoleRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlAuthorRepository>().As<IAuthorRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlBookAuthorRepository>().As<IBookAuthorRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlBookRepository>().As<IBookRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlCategoryRepository>().As<ICategoryRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlFacultyRepository>().As<IFacultyRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlGroupRepository>().As<IGroupRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlLanguageRepository>().As<ILanguageRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlPublicationRepository>().As<IPublicationRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlSectorRepository>().As<ISectorRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlSpecialtyRepository>().As<ISpecialtyRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlStudentRepository>().As<IStudentRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlUserRepository>().As<IUserRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlFileTypeRepository>().As<IFileTypeRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
            builder.RegisterType<SqlActivityRepository>().As<IActivityRepository>()
                .WithParameter("connectionString", _connectionString).SingleInstance();
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
