using JuniorStart.DTO;
using JuniorStart.Filters;
using JuniorStart.Services.Interfaces;
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
        private readonly IRecruitmentService _service;

        public RecruitmentInfoController(IRecruitmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all recruitments for current user
        /// </summary>
        /// <param name="ownerId">Current user id</param>
        /// <returns></returns>
        /// <response code="200">Returns recruitment info</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("{ownerId}/recruitments")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetRecruitmentInfoForUser(int ownerId)
        {
            return Ok(_service.GetRecruitmentsForUser(ownerId));
        }

        /// <summary>
        /// Get information about single recruitment
        /// </summary>
        /// <param name="id">Recruitment Id</param>
        /// <returns>Returns recruitment info</returns>
        /// <response code="200">Returns recruitment info</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("{id}", Name = "GetRecruitmentInfoById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetRecruitmentInfoById(id));
        }

        /// <summary>
        /// Add new recruitment Info
        /// </summary>
        /// <param name="requestModel">request object</param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [ModelValidation]
        [HttpPost(Name = "addInformation")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] RecruitmentInformationViewModel requestModel)
        {
            _ = _service.CreateRecruitmentInfo(requestModel);
            return CreatedAtRoute("GetRecruitmentInfoById", new {id = requestModel.Id}, requestModel);
        }

        /// <summary>
        /// Update recruitment info
        /// </summary>
        /// <param name="requestModel">Model to update</param>
        /// <param name="recruitmentId">Id of model</param>
        /// <response code="200">Returns updated object</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [ModelValidation]
        [HttpPut("{id}", Name = "updateInformation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] RecruitmentInformationViewModel requestModel, int recruitmentId)
        {
            _ = _service.UpdateRecruitmentInfo(recruitmentId, requestModel);
            return StatusCode(200, requestModel);
        }

        /// <summary>
        /// Archives recruitment info
        /// </summary>
        /// <param name="id">Recruitment id</param>
        /// <returns></returns>
        /// <response code="204">Returns if successfully archived object</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpDelete("{id}", Name = "deleteInformation")]
        public IActionResult Delete(int id)
        {
            _service.ArchiveRecruitmentInfo(id);
            return NoContent();
        }
    }
}