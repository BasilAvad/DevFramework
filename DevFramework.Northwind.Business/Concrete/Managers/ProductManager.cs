using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.Aspects.PostSharp;
using DevFramework.Core.DataAccess;
using System.Transactions;
using DevFramework.Core.Aspects.PostSharp.TrnsactionAspect;
using DevFramework.Core.Aspects.PostSharp.CacheAspect;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.PostSharp.LogAspects;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
            
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            
            return _productDal.Update(product);
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCachManager))]
        public Product Add(Product product)
        {
           
            return _productDal.Add(product);
        }


        [CacheAspect(typeof(MemoryCachManager))]
        [LogAspect(typeof (DatabaseLogger))]
        [LogAspect(typeof (FileLogger))]
        List<Product> IProductService.GetAll()
        {
           
            return _productDal.GitList();
        }

        Product IProductService.GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }
        [TrnsactionScopeAspect]
        public void TrnsactionOpretion(Product product1, Product product2)
        {
            
                 _productDal.Add(product1);
                    // 
                    _productDal.Add(product2);
                    
        }
    }
}
