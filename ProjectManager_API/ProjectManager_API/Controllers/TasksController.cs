using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager_API.Models.DTO;

namespace ProjectManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ProjectManagerContext _context;

        public TasksController(ProjectManagerContext context)
        {
            _context = context;
        }

        // GET: api/Tasks/GetAllTasksByProjectId/projectId
        [Route("GetAllTasksByProjectId/{projectId}")]
        [HttpGet]
        public IEnumerable<Models.DTO.Task> GetAllTasksByProjectId(Guid projectId)
        {
            IEnumerable<Models.DTO.Task> taskList = _context.Tasks.
                                            Include(t => t.Project).Where(p=>p.ProjectId == projectId)
                                            .OrderByDescending(t => t.WorkHours)
                                            .ThenByDescending(t=>t.CreatedDate)
                                            .ToList();
            return taskList;
        }

        // GET: api/Tasks/GeTaskById?Id={id}
        [Route("GeTaskById")]
        [HttpGet]
        public ActionResult<Models.DTO.Task> GeTaskById(Guid id)
        {
            Models.DTO.Task task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/Tasks/PostTaskInProject
        [Route("PostTaskInProject")]
        [HttpPost]
        public ActionResult PostTaskInProject([FromBody] Models.DTO.Task taskForPost)
        {
            try
            {
                _context.Tasks.Add(taskForPost);
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: insert logs in registers or in file
                return StatusCode(500);
            }
        }

        // PUT: api/Tasks/EditTaskById
        [Route("EditTaskById/{id}/{projectId}")]
        [HttpPut]
        public ActionResult EditTaskById(Guid id,Guid projectId, [FromBody]Models.DTO.Task taskForEdit)
        {
            if (taskForEdit.Id != id && taskForEdit.ProjectId != projectId)
            {
                return BadRequest();
            }
            //if (_context.Projects.Any(e => e.Id == projectForEdit.Id))
            //{
            //    return NotFound();
            //}
            _context.Entry(taskForEdit).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
            return Ok();
        }

        // DELETE: api/Tasks/DeleteTaskById/id
        [Route("DeleteTaskById/{id}")]
        [HttpDelete]
        public ActionResult DeleteTaskById(Guid id)
        {
            Models.DTO.Task taskForDelete = _context.Tasks.Find(id);
            if (taskForDelete == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(taskForDelete);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
