using System;
using COMP2139_labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comp_2139.Areas.ProjectManagement.Components.ProjectSummary
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        //Async --> non blocking
        public async Task<IViewComponentResult> InvokeAsync(int projectID)
        {

            var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .FirstOrDefaultAsync(p => p.ProjectId == projectID);

            if (project == null)
            {
                //Handle case when the project is not found, return html content
                return Content("Project Not Found");
            }

            return View(project);

        }

    }
}




