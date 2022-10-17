using Castle.DynamicProxy;
using Library.Core.CrossCuttingConcerns.Logging;
using Library.Core.CrossCuttingConcerns.Logging.Log4Net;
using Library.Core.Interceptors;

namespace Library.Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerService;

        public LogAspect(Type loggerServiceBase)
        {
            if (loggerServiceBase.BaseType != typeof(LoggerServiceBase))
                throw new System.Exception("Wrong logger type");
            _loggerService = (LoggerServiceBase)Activator.CreateInstance(loggerServiceBase);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerService.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
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

            var logDetail = new LogDetail
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name
            };

            return logDetail;
        }
    }
}