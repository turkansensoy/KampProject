using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
  public class ProductValidator:AbstractValidator<Product>
    {
        //RulerFor AbstractValidator<Product> 'ın içindeki nesne ile ilgilidir.karşılık gelir.
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p=>p.ProductName).MinimumLength(2);
            RuleFor(p=>p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0); //sıfırdan farkli
            RuleFor(p=>p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürüünler A harfi ile başlamalı");


        }
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
