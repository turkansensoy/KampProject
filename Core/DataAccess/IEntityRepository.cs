using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //ICategoryDal ve IProductDal interfacelerin aynı yapıyı taşımaması için Generic yapılarıları kullanabiliriz
    // generic constraint denir
    //class: referans tip
    //IEntity:IEntity olabilir veye IEntity implemente eden bir nesne olabilir. IEntity new'lenemez interface çünkü
    //new(): new'lenebilir olmalı
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        // Ürünlerin tamamını degilde bazılarını filtreleme yapmak için Expression<> diye bir yapı kullanılır.
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
