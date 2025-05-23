﻿using Castle.DynamicProxy;

namespace Library.Core.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }

        protected virtual void OnException(IInvocation invocation, Exception ex)
        {
            string filePath = @"C:\Log\logs.txt";
            string content =
                $"-------------\n\nException Date: {DateTime.Now.ToString("g")}\nException Message: {ex.Message}\n\n" +
                $"-------------\n\n";
            File.AppendAllText(filePath, content);
        }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            bool isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation, ex);
                throw;
            }
            finally
            {
                if (isSuccess) OnSuccess(invocation);
            }
            OnAfter(invocation);
        }
    }
}
