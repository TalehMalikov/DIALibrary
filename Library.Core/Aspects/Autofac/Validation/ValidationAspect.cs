using FluentValidation;
using Library.Core.Interceptors;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;

namespace Library.Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                throw new Exception("Parameter must assignable from IValidator");
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(a => a.GetType() == entityType).ToList();
            foreach (var entity in entities)
                ValidatorTool.Validate(entity, validator);
        }
    }
}
