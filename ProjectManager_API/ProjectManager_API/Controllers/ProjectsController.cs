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
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectManagerContext _context;

        public ProjectsController(ProjectManagerContext context)
        {
            _context = context;
        }

        // GET: api/Projects/GetAllProjects
        [Route("GetAllProjects")]
        [HttpGet]
        public IEnumerable<Project> GetAllProjects()
        {
            IEnumerable<Project> projectsList = _context.Projects.
                                            Include(p => p.Tasks)
                                            .OrderByDescending(p => p.CreateDate)
                                            .ThenByDescending(p => p.Tasks.Count())
                                            .ToList();

            return projectsList;
        }

        // GET: api/Projects/GetProjectById/id
        [Route("GetProjectById/{id}")]
        [HttpGet]
        public ActionResult<Project> GetProjectById(Guid id)
        {
            Project project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            project = _context.Projects
                        .Include(p => p.Tasks)
                        .FirstOrDefault(p => p.Id == id);
            return project;
        }

        // POST: api/Projects/PostProjectInDB
        [Route("PostProject")]
        [HttpPost]
        public ActionResult PostProject([FromBody]Project projectForPost)
        {
            try
            {
                _context.Projects.Add(projectForPost);
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: insert logs in registers or in file
                return StatusCode(500);
            }
        }

        // PUT: api/Projects/EditProject/id
        [Route("EditProject/{id}")]
        [HttpPut]
        public ActionResult EditProject(Guid id, [FromBody]Project projectForEdit)
        {
            if (projectForEdit.Id != id)
            {
                return BadRequest();
            }
            _context.Entry(projectForEdit).State = EntityState.Modified;
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

        // DELETE: api/DeleteProjectById/id
        [Route("DeleteProjectById/{id}")]
        [HttpDelete]
        public ActionResult DeleteProjectById(Guid id)
        {
            try
            {
                Project projectForDelete = _context.Projects.Find(id);
                if (projectForDelete == null)
                {
                    return NotFound();

                }
                projectForDelete = _context.Projects
                                    .Include(p => p.Tasks)
                                    .FirstOrDefault(p => p.Id == id);
                if (projectForDelete.Tasks.Count == 0)
                {
                    _context.Projects.Remove(projectForDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return StatusCode(403, "The project has assigned tasks. Can not be deleted");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
           
        }
    }
}
