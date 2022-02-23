using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [ApiController]  =ATTRIBUTE
    // controller:Gelen bütün istekleri kontroller karşılıyor
    //controller, sistemimizi kullanacak client'lar,Mobli uygulama olabilir,Desktop uygulama olabilir.web uyugulaması olabilir,
    //angular,react uygulaması olabilir.
    // [ApiController] c#'da ATTRIBUTE,JAVA'DA ANNOTATION
    // ATTRIBUTE bir class ile ilgili ona bilgi verme,imzalama yöntemidir
    // restfull yapılar http protokol üzerinden geliyor.bir kaynaga ulaşmak için izlenen yol.
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
           var result= _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
