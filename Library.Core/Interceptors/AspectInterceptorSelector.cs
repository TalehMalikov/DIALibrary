using System.Reflection;
using Castle.DynamicProxy;
using Library.Core.Aspects.Autofac.Exception;
using Library.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Library.Core.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            classAttributes.AddRange(methodAttributes);
            return classAttributes.ToArray();
        }
    }
}
