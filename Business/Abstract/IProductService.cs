using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        //LisList<Product> yerine  IDataResult yazılır.
        //IDataResult hem işlem sonucunu,hem messajı içeren hemde döndürecegi şeyi
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(Decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product); 
        //transaction yönetimi uygulamalarda tutarlıgı korumak için yaptıgımız yöntem
    }
}
