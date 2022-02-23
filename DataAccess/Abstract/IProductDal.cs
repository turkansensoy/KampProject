using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    // I interface, Product Hangi Tabloya karşılık geldigini  entity anlatır.Dal ise onun hangi katmana karşılık geldigini
    // Dal Data acces layer.data acsess katmanının nesnesidir
    //interfacenın kendisi public degil.operasyonları publictir.
    // IProductDal = veri erişim katmanıdır.
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
    }
}
