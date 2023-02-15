using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace APIOData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ODataController
    {

        [HttpGet]
        [ODataRoute("GetKdv")]
        public IActionResult GetKdv()
        {
            return Ok(18);
        }
    }
}
