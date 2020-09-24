using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using DevFramework.Core.CrossCuttingConcerns.Caching;
using System.Reflection;

namespace DevFramework.Core.Aspects.PostSharp.CacheAspect
{
    [Serializable]
    public class CacheAspect:MethodInterceptionAspect
    {
        private Type _cacheType;
        int _cacheDyMinute;
        ICachingManager _cachingManager;

        public CacheAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public CacheAspect(int cacheDyMinute)
        {
            _cacheDyMinute = cacheDyMinute;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            // gonderelen _cacheType bir ICachingManager turunde degelse 
            if (typeof(ICachingManager).IsAssignableFrom(_cacheType)==false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cachingManager = (ICachingManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",
                args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();
            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            if (_cachingManager.IsAdd(key))
            {
                args.ReturnValue = _cachingManager.Get<object>(key);
            }
            _cachingManager.Add(key, args.ReturnValue, _cacheDyMinute);
            base.OnInvoke(args);
        }
    }
}
