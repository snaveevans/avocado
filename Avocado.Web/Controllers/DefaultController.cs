using System;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    public class DefaultController : Controller
    {
        [HttpGet("/version")]
        public IActionResult Version()
        {
            return Ok(new
            {
                Version = "0.0.1",
                Date = DateTime.UtcNow
            });
        }
    }
}