using Business_Logic_Layer.Models.Request.Interfaces;
using Business_Logic_Layer.Services.AbstactClasses;
using Business_Logic_Layer.Services.ConcreteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoftoneStudentManagmentSystem.Controllers
{

    /*Developer:Buddhika Walpita
      Date:2025.05.29
      Description:Student API controller for managing student data
    
    */
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly AStudentService stuService;

        #region Dependancy Injection
        //Apply dependancy injection
        public StudentApiController(AStudentService _stuService)
        {
            this.stuService = _stuService;
        }
        #endregion

        #region Get Student Data

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()  //Get All Student Data
        {
            try
            {
                var result = await this.stuService.GetStudents();
                if (result == null || result.Count == 0)
                    return NotFound();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentsByKeyAsync(Guid id)  //Get student data by Id
        {
            try
            {
                var result = await this.stuService.GetStudentsbyId(id);
                if (result == null || result.Count == 0)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        //[HttpGet("{search:string}")]
        public async Task<IActionResult> GetStudentsBySearchTextAsync(string search)  //Get student data by Search Text
        {
            try
            {
                var result = await this.stuService.GetStudentsbyText(search);
                if (result == null || result.Count == 0)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Update Student Data
        [HttpPost]
        public async Task<IActionResult> SaveStudentData([FromBody] IStudentRequest stuRequest)  //Save Student data
        {
            try
            {
                if (stuRequest == null)
                {
                    return BadRequest("Student Data is empty");
                }

                var result = await this.stuService.SaveStudentData(stuRequest);
                if (result <= 0)
                {
                    return NotFound("Student not found");
                }
                return Ok("Student saved successfully");
        
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        #endregion

        #region Delete Student Data
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult>  DeleteStudentData(Guid id)  //Delete Student data
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid student ID");
                }

                var result = await this.stuService.DeleteStudentData(id);
                if (result <= 0)
                {
                    return NotFound("Student not found");
                }
                return Ok("Student deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

    }
}
