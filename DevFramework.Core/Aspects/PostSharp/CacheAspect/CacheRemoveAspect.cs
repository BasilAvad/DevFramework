using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.CacheAspect
{
    [Serializable]
   public class CacheRemoveAspect:OnMethodBoundaryAspect
    {
        // Cacheten silmek için iki yontemiz olacak birincesi bir Manager bütün Cecheleri silmek diğeride bizim veriğimiz batern uygun yani Cache Yada Cacheleri sil .
        private string _pattern;
        private  Type _cacheType;
        private  ICachingManager _cachingManager;
        public CacheRemoveAspect(Type cachetype)
        {
            _cacheType = cachetype;
        }
        public CacheRemoveAspect(string pattern,Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            // gonderelen _cacheType bir ICachingManager turunde degelse 
            if (typeof(ICachingManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cachingManager = (ICachingManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cachingManager.RemoveByPattern(string.IsNullOrEmpty(_pattern)
                ?string.Format("{0}.{1}.*",args.Method.ReflectedType.Namespace,args.Method.ReflectedType.Name)
                :_pattern);
        }

    }
}
