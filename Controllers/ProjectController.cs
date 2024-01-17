using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2139_labs.Models;
using Microsoft.AspNetCore.Mvc;


namespace COMP2139_labs.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name = "Project 1", Description = " My First Project"}
            };
            return View(projects);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }

    }
}

