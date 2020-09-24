using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.ValidationRules.FluentValidation
{
    public  class ProductValidatior:AbstractValidator<Product>
    {
        public ProductValidatior()
        {
            // iş kuralları burada yazılır 
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage(" boş olmaz "); // CategoryId mutlaka girilmeli 
            RuleFor(P => P.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.QuantityPerUnit).NotEmpty();
            RuleFor(p => p.ProductName).Length(2, 20); // من 2الى 20 حرف
            RuleFor(p => p.UnitPrice).GreaterThan(20).When(p => p.CategoryId == 1);
            //RuleFor(p => p.ProductName).Must(StartWithA);

        }

        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
