using DevFramework.Northwind.Entities.Concrete;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Mappings
{
    public class categoryMap : ClassMap<Category>
    {
        public categoryMap()
        {
            Table(@"Categories");
            LazyLoad();
            Id(X => X.CategoryId).Column("CategoryID");

            Map(x => x.CategoryName).Column("CategoryName");
            
        }

    }
}
