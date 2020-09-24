using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess
{
    /// <summary>
    /// bütün nesnelrim için veri taban nesnelerim  generik çalişacak bir model oluşturyorum 
    /// </summary>
    public interface IEntityRepository<T> where T:class ,IEntity,new()
    {
        List<T> GitList(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        // entity gonderip tum hizmet etmek için bazıları string bazıları int bu yuzden 

    }
}
