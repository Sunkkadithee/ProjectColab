﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2139_labs.Data;
using COMP2139_labs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_labs.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound(); //444
            }

            return View(project);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
        {
            if(id != project.ProjectId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
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
                return RedirectToAction("Index");
                
            }
            return View(project);
        }


        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {

            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound(); //444
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }


        public async Task<IActionResult> Search(string searchString)
        {
            var projectQuery = from p in _context.Projects
                               select p;
            bool seaarchPerformed = !String.IsNullOrEmpty(searchString);

            if (seaarchPerformed)
            {
                projectQuery = projectQuery.Where(p => p.Name.Contains(searchString)
                                                        || p.Description.Contains(searchString));
            }
            var projects = await projectQuery.ToListAsync();
            ViewData["SearchPerformed"] = seaarchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", projects);
        }

    }
}

