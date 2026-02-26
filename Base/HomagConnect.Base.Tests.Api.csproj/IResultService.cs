using Microsoft.AspNetCore.Mvc;

namespace HomagConnect.Base.Tests.Api
{
    public interface IResultService
    {
        Task<IActionResult> GetResult(HttpContext context);
    }
}
