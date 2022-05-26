using APIOData.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIOData.API.Controllers
{
 
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

        [ODataRoute("Products({Item})")]
        [EnableQuery]
        public IActionResult GetProducts([FromODataUri] int key)
        {
            return Ok(_context.Products.Where(x => x.Id == key));
        }
    }
}
