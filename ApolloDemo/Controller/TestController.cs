using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloDemo.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string Index()
        {
            return ConfigHelper.Instance["ApolloDemoConfig:ConnectingString"];
        }
    }
}
