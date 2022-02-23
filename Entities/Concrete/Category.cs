using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak class kalmasın. Eger ki bir class inheritıs veya interface intlemetasyonu almıyor ise problem yaşanır. 
    public class Category:IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
