using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MySchoolAPI.Data.Interfaces;
using MySchoolAPI.DTOs;
using MySchoolAPI.Uilities;
using MySchoolAPI.Uilities.TimerFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepository student;
        private readonly IHubContext<HubAlert> hub;

        public StudentController(IStudentRepository studentRepository, IHubContext<HubAlert> hubContext)
        {
            this.student = studentRepository;
            this.hub = hubContext;
        }


        /// <summary>
        /// Get All The Students
        /// </summary>
        /// <returns> Students </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {

                //var timermanager = new TimerManager(() => hub.Clients.All.SendAsync("GetAllData"));
                return Ok(await student.GetAll());
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }



        /// <summary>
        /// Create New Student
        /// </summary>
        /// <param name="studentCreationDTO"></param>
        /// <returns> New Student </returns>
        [HttpPost]
        public IActionResult Post([FromBody] StudentCreationDTO studentCreationDTO) 
        {

            try
            {

                hub.Clients.All.SendAsync("StudentCreated", studentCreationDTO);
                
                return Ok(student.Create(studentCreationDTO));
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }



        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns> True </returns>
        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {

            try
            {
                
                hub.Clients.All.SendAsync("StudentDeleted", id);

                return Ok(student.Delete(id));
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }



        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDTO"></param>
        /// <returns> Id </returns>
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] StudentDTO studentDTO)
        {

            try
            {
                hub.Clients.All.SendAsync("StudentUpdated", id);

                return Ok(student.Update(id,studentDTO));
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
        
        
        
        /// <summary>
        /// Search Student - 01-02-2023
        /// </summary>
        /// <param name="searchStudent"></param>
        /// <returns> FirstName or PhoneNumber </returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string searchStudent)
        {

            try
            {
                //hub.Clients.All.SendAsync("SearchStudent",searchStudent);

                return Ok(await student.Search(searchStudent));
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
