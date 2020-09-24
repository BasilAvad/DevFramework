using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
namespace DevFramework.Core.Aspects.PostSharp
{
    [Serializable]
    public  class FluentValidationAspect:OnMethodBoundaryAspect
    { 
        Type _validatorType;
            public FluentValidationAspect (Type validatorType)
            {
                _validatorType = validatorType;
            }
        public override void OnEntry(MethodExecutionArgs args)
        {
            // base.OnEntry(args); // Method girdiğimizde doğrulama yapsın 
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // burada Product nesnesi ile çalışacağımı biliyorum
            var entities = args.Arguments.Where(t => t.GetType() == entityType);// Product Managerde çaliştiğim metodun parametreleri gezip tipi product olanları yakalanıyorum
            foreach (var entity in entities )
            {
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
