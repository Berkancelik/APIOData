using APIOData.API.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
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
        public IActionResult GetProducts([FromODataUri]int key)
        {
            return Ok(_context.Products.Where(x => x.Id == key));
        }

        // isimlendirme isimleri rastgele isimler değildir
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult PutProduct([FromODataUri] int key, [FromBody] Product product)
        {
            product.Id = key;
            // güncellenmiş yani modified olmuş anlamına gelir
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return NoContent();

        }

    }
}
