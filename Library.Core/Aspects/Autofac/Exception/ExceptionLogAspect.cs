using Castle.DynamicProxy;
using Library.Core.CrossCuttingConcerns.Logging;
using Library.Core.CrossCuttingConcerns.Logging.Log4Net;
using Library.Core.Interceptors;

namespace Library.Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerService;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new System.Exception("Wrong logger type");
            _loggerService = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnException(IInvocation invocation, System.Exception ex)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = ex.Message;
            _loggerService.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Type = invocation.Arguments[i].GetType().Name,
                    Value = invocation.Arguments[i]
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name
            };
            return logDetailWithException;
        }
    }
}