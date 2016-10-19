using Microsoft.AspNetCore.Mvc;
using ReciPiBook.Services;

namespace ReciPiBook.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public string GetHello()
        {
            return _testService.SayHello();
        }
    }
}
