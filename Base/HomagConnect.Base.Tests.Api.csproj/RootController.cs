using Microsoft.AspNetCore.Mvc;

namespace HomagConnect.Base.Tests.Api
{
    public class RootController : ControllerBase
    {
        private readonly IResultService _resultService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultService"></param>
        public RootController(IResultService resultService)
        {
            _resultService = resultService ?? throw new ArgumentNullException(nameof(resultService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _resultService.GetResult(HttpContext);
            return res;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var res = await _resultService.GetResult(HttpContext);
            return res;
        }
    }
}
