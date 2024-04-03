using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2139_labs.Data;
using COMP2139_labs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_labs.Areas.ProjectManagement.Models;

namespace COMP2139_labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }


        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound(); //444
            }

            return View(project);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Create")]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description, EndDate, StartDate, Status")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // Processes the deletion of a project
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectId)
        {
            var project = _context.Projects.Find(projectId);

            if (project != null)
            {

                _context.Projects.Remove(project);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the list of projects
            }

            return NotFound();
        }

        //[HttpGet("Search/{searchString?}")]
        //public async Task<IActionResult> Search(string searchString)
        //{
        //    var projectQuery = from p
        //                       in _context.Projects
        //                       select p;
        //    bool searchPerformed = !String.IsNullOrEmpty(searchString);

        //    if (searchPerformed)
        //    {
        //        projectQuery = projectQuery.Where(p =>
        //        p.Name.Contains(searchString) ||
        //        p.Description.Contains(searchString));
        //    }

        //    var projects = await projectQuery.ToListAsync();
        //    ViewData["SearchPerformed"] = searchPerformed;
        //    ViewData["SearchString"] = searchString;

        //    return View("Index", projects); // Reuse index View to Display Results
        //}

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string searchString)
        {
            var projectQuery = from p
                               in _context.Projects
                               select p;
            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            if (searchPerformed)
            {
                projectQuery = projectQuery.Where(p =>
                p.Name.Contains(searchString) ||
                p.Description.Contains(searchString));
            }

            var projects = await projectQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;

            return View("Index", projects); // Reuse index View to Display Results
        }

    }
}

