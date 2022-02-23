using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Aspects.Autofac.Caching;
using System.Transactions;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class ProductManager : IProductService    //interface'ler referans tutucudur
    {
        IProductDal _ProductDal; //injection yapma
        // bir entityManager başka entity'i injecte edemez.sadece IproductDal'ı yapabilir.başka bir dal eklenemez
        //iş katmanı iş süreçlerinin yazıldıgı yer
        ICategoryService _categoryService;
        // bir entityManager başka entity'i injecte edemez.sadece IproductDal'ı yapabilir.başka bir dal eklenemez
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _ProductDal = productDal;
            _categoryService = categoryService;
        }
        //loglama bir yerde yapılan operasyonun kaydını tutmaktır.başında,bitiminde calıştırılabilir.
        /*
       [LogAspect] --> // AOP bir metodun önünde,sonunda,bir metot hata verdiginde calışan kod parcacıklarına AOP mimarisi ile yazılıyor.
                       // Yani business içerisine bussines yazılır.
       [Validate] -->//Dogrula.ürün eklenecek kuralları dogrula
       [Transaction] //Hata olursa geri al
         //bunlara AOP denir */

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business kod =iş gereksinimler,ihtiyaclarımıza uygunluktur.
            //validation=doğrulama eklemeye calışılan varlık product nesneyi iş kurallarına dahil etmek için yapısal olarak 
            //uygun olup olmadıgı kontrl etmeye dogrulama denir.
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductExists(product.ProductName), CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }
            _ProductDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }
        [CacheAspect] //key value
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);//MaintenanceTime bakım zamanı.Ampulden Generate field yapıyoruz
            }

            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.CategoryId == id));
        }
        [CacheAspect]
       // [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_ProductDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails());
        }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _ProductDal.Update(product);
            return new SuccessResult();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _ProductDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductExists(string productName)
        {
            var result = _ProductDal.GetAll(p => p.ProductName == productName);
            if (result.Any())
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        //Eger mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.
        private IResult CheckIfCategoryLimitExceded()
        {
            var results = _categoryService.GetAll();
            if (results.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
}