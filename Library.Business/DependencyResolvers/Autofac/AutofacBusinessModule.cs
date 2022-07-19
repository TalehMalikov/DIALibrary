using Autofac;
using Library.DataAccess.Abstraction;
using Library.DataAccess.Implementation.PostgreSql;

namespace Library.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            #region DataAccess
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
            builder.RegisterType<SqlSpecialtyRepository>().As<ISpecialtyRepository>().SingleInstance();
            builder.RegisterType<SqlStudentRepository>().As<IStudentRepository>().SingleInstance();
            builder.RegisterType<SqlUserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<SqlUnitOfWork>().As<IUnitOfWork>().SingleInstance();
            #endregion

        }
    }
}
