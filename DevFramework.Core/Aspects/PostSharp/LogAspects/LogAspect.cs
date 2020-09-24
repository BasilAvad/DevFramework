using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using System.Reflection;
using DevFramework.Core.CrossCuttingConcerns.Logging;

namespace DevFramework.Core.Aspects.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method,TargetParameterAttributes =MulticastAttributes.Instance)]
   public class LogAspect:OnMethodBoundaryAspect
    {
        private Type _logerType;
       private LoggerService _loogerService;
        public LogAspect(Type logerType)
        {
            _logerType = logerType;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_logerType.BaseType!=typeof(LoggerService))
            {
                throw new Exception("Worng Logger Type ");
            }
            _loogerService = (LoggerService)Activator.CreateInstance(_logerType);
            base.RuntimeInitialize(method);
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loogerService.IsInfoEnabled)
            {
                return;
            }

            try
            {
                var LogParameter = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();

                var LoDetail = new LogDitail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = LogParameter,
                };
                _loogerService.Info(LoDetail);
            }
            catch (Exception)
            {

            }
        }
    }
}
