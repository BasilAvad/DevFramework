using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EfQueryableRepositroy<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _entities; // bu _entities dolduracak bir yapı ihtiyacım var

        public EfQueryableRepositroy(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table { get { return this.Entities; } }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities==null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}
