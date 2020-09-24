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
    public class ProductMap:ClassMap<Product>
    {
        public ProductMap()
        { 
            Table(@"Products");
            LazyLoad();
            Id(X => X.ProductId).Column("ProductID");

            Map(x => x.CategoryId).Column("CategoryId"); 
            Map(x => x.ProductName).Column("ProductName");
            Map(x => x.QuantityPerUnit).Column("QuantityPerUnit");
            Map(x => x.UnitPrice).Column("UnitPrice");
        }
       
    }
}
