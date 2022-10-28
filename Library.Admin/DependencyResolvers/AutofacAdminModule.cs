using Autofac;
using Library.Admin.Services.Abstract;
using Library.Admin.Services.Concrete;

namespace Library.Admin.DependencyResolvers
{
    public class AutofacAdminModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<LanguageService>().As<ILanguageService>().SingleInstance();
            builder.RegisterType<SpecialtyService>().As<ISpecialtyService>().SingleInstance();
            builder.RegisterType<FacultyService>().As<IFacultyService>().SingleInstance();
            builder.RegisterType<GroupService>().As<IGroupService>().SingleInstance();
            builder.RegisterType<SectorService>().As<ISectorService>().SingleInstance();
            builder.RegisterType<FileTypeSevice>().As<IFileTypeService>().SingleInstance();
            builder.RegisterType<EducationalProgramService>().As<IEducationalProgramService>().SingleInstance();
            builder.RegisterType<AuthorService>().As<IAuthorService>().SingleInstance();
            builder.RegisterType<AccountRoleService>().As<IAccountRoleService>().SingleInstance();
            builder.RegisterType<FileAuthorService>().As<IFileAuthorService>().SingleInstance();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        }
    }
}
