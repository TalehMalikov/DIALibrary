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
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
        }
    }
}
