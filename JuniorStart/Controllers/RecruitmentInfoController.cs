using System.Collections.Generic;
using JuniorStart.DTO;
using JuniorStart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorStart.Controllers
{
    /// <summary>
    /// Recruitment Information
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("api/recruitment")]
    [Authorize]
    public class RecruitmentInfoController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;

        public RecruitmentInfoController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        /// <summary>
        /// Get all recruitments for current user
        /// </summary>
        /// <param name="ownerId">Current user id</param>
        /// <returns></returns>
        /// <response code="200">Returns recruitment info</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("{ownerId}/recruitments")]
        [ProducesResponseType(typeof(List<RecruitmentInformationDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult GetRecruitmentInfoForUser(int ownerId)
        {
            return Ok(_recruitmentService.GetRecruitmentsForUser(ownerId));
        }

        /// <summary>
        /// Get information about single recruitment
        /// </summary>
        /// <param name="id">Recruitment Id</param>
        /// <returns>Returns recruitment info</returns>
        /// <response code="200">Returns recruitment info</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet("{id}", Name = "GetRecruitmentInfoById")]
        [ProducesResponseType(typeof(RecruitmentInformationDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            return Ok(_recruitmentService.GetRecruitmentInfoById(id));
        }

        /// <summary>
        /// Add new recruitment Info
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/recruitment
        ///     {
        ///        "companyName": "Test",
        ///        "workPlace": "Developer",
        ///        "notes": "Good interview",
        ///        "dateOfCompanyReply": "24.07.2019",
        ///        "linkToApplication": "https://google.com",
        ///        "ownerId": 1
        ///     }
        /// </remarks>
        /// <param name="requestModel">request object</param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPost(Name = "addInformation")]
        [ProducesResponseType(typeof(RecruitmentInformationDto), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] RecruitmentInformationDto requestModel)
        {
            _ = _recruitmentService.CreateRecruitmentInfo(requestModel);
            return CreatedAtRoute("GetRecruitmentInfoById", new {id = requestModel.Id}, requestModel);
        }

        /// <summary>
        /// Update recruitment info
        /// </summary>
        /// <param name="requestModel">Model to update</param>
        /// <param name="recruitmentId">Id of model</param>
        /// <response code="200">Returns updated object</response>
        /// <response code="400">If the item is not valid.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPut("{id}", Name = "updateInformation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] RecruitmentInformationDto requestModel, int recruitmentId)
        {
            _ = _recruitmentService.UpdateRecruitmentInfo(recruitmentId, requestModel);
            return StatusCode(200, requestModel);
        }

        /// <summary>
        /// Archives recruitment info
        /// </summary>
        /// <param name="id">Recruitment id</param>
        /// <returns></returns>
        /// <response code="204">Returns if successfully archived object</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If unexpected error appear</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}", Name = "deleteInformation")]
        public IActionResult Delete(int id)
        {
            _recruitmentService.ArchiveRecruitmentInfo(id);
            return NoContent();
        }
    }
}