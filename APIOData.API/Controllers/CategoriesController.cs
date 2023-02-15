using APIOData.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIOData.API.Controllers
{
    [ODataRoutePrefix("Products")]
    public class CategoriesController : ODataController
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }


        // client nasıl bir sorgulama istiyor ise o o sorgulamaya uygun data dönevektir.
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Categories);
        }

        [EnableQuery(PageSize = 2)]
        public IActionResult GetProducts([FromODataUri] int key)
        {
            return Ok(_context.Products.AsQueryable());
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("Categories({id}/products({item}))")]
        public IActionResult ProdcutById([FromODataUri] int id, [FromODataUri] int item)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id && x.Id == item));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetProducts2([FromODataUri] int id)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id));
        }



        [HttpPost]
        public IActionResult TotalProductPrice([FromODataUri] int key)
        {
            var total = _context.Products.Where(x => x.CategoryId == key).Sum(x => x.Price);
            return Ok(total);
        }


        [HttpPost]
        public IActionResult TotalProductPrice2()
        {
            var total = _context.Products.Sum(x => x.Price);
            return Ok(total);
        }

        [HttpPost]
        public IActionResult TotalProductWithParametre(ODataActionParameters parameters)
        {
            int categoryId = (int)parameters["categoryId"];
            var total = _context.Products.Where(x => x.CategoryId == categoryId).Sum(x => x.Price);
            return Ok(total);

        }

        [HttpGet]
        public IActionResult CategoryCount()
        {
            var count = _context.Categories.Count();
            return Ok(count);
        }



    }
}

