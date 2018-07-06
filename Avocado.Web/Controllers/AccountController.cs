using Avocado.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avocado.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        
        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }
    }
}