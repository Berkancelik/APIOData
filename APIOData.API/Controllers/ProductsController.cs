using APIOData.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIOData.API.Controllers
{

    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Products.AsQueryable());
        }

        [EnableQuery]
        public IActionResult GetProducts([FromODataUri] int key)
        {
            return Ok(_context.Products.Where(x => x.Id == key));
        }

        [ODataRoute("Products")]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromODataUri] int key, [FromBody] Product product)
        {
            product.Id = key;
            // güncellenmiş yani modified olmuş anlamına gelir
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromODataUri] int key)
        {
            var product = _context.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public IActionResult Login(ODataActionParameters parameters)
        {
            Login login = parameters["UserLogin"] as Login;

            return Ok(login.Email + " -  " + login.Password);
        }


        //products/multiplayFunction()
        [HttpGet]
        public IActionResult MultiplyFuncction([FromODataUri] int a1, [FromODataUri] int a2, [FromODataUri] int a3)
        {
            return Ok(a1 * a2 * a3);
        }


    }
}
