using Avocado.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        
        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }
    }
}