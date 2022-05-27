using APIOData.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
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
        public IActionResult ProdcutById([FromODataUri]int id,[FromODataUri] int item)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id && x.Id == item));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetProducts2([FromODataUri] int id)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id ));
        }


     
    }
}

