using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace VaultDemo.Controllers
{
    [ApiController]
    [Route("vault")]
    public class VaultController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public VaultController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public ActionResult<Data> GetValue()
        {
            var val = configuration.GetValue<string>("Fooo");
            if (string.IsNullOrEmpty(val))
                return NotFound();
            else
                return Ok(new Data(val));
        }
    }
}