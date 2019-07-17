using JuniorStart.DTO;
using JuniorStart.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorStart.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/recruitment")]
    [AuthorizeAttribute]
    public class RecruitmentInfoController : ControllerBase
    {
        [ModelValidation]
        [HttpPost("addInformation")]
        public IActionResult Post([FromBody] RecruitmentInformationViewModel postModel)
        {
            return Ok();
        }
    }
}