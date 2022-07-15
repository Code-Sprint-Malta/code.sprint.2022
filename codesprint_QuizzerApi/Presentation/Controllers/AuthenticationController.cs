using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult LoginWithGoogle()
        {
            return Ok(User.Identity.Name);
        }
    }
}