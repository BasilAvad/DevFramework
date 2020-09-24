using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching
{
    public interface ICachingManager
    {
        T Get<T>(string key);
        void Add(string key,object data,int cachTime);
        // Cach Datada ne kadar kalacak
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string Pattern);
        void Clear();

    }
}
