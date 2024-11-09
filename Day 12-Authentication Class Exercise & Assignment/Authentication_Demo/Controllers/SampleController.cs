
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [Authorize(Roles = "User")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<string> GetSampleData()
        {
            return "Sample data from from Sample Controller";
        }
    }
}





//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using System.Security.Claims;

//namespace Authentication_Demo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SampleController : ControllerBase
//    {
//        [Authorize(Roles = "User,Admin")]
//        [HttpGet("data")]
//        public async Task<string> GetUserSampleData()
//        {
//            if (User.IsInRole("Admin"))
//            {
//                return "Hello Admin from Sample Controller";
//            }
//            else if (User.IsInRole("User"))
//            {
//                return "Sample data for User role from Sample Controller";
//            }
//            else
//            {
//                return "Access Denied";
//            }
//        }
//    }
//}


