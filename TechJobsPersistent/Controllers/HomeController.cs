using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {/* 
            No exact code to borrow here either but there is something similar in the AddJob in our skill controller...
            
            To add a job we're going to pass an instance of an AddJobViewModel back to the view

             */
            return View();
        }

        public IActionResult ProcessAddJobForm()
        {

            /* So this will be kinda Like ProcessAddEmployerForm that you did in the Employer Controller
            
            This action needs to take in an instance of the AddJobViewModel and
            
            IF the model is valid then
                create a Job object that 
                    will pass the properties (Name, EmployerId, and Employer) from the ViewModel that it's taking in.

                Then we need to add this new Employer object that we've crated to the Employers table in the Db
                   Then save the changes to the Db
                   when all that works we'll redirect to see the list of Employers

            If none of that happens (cause the model isn't valid), then we want to go to the Add view and 
            pass it the information that is in the View Model from the attempted addition

      
            When we test this at some point our dropdown (SelectListItem) is going to show up empty. 
            so we have to add some context for when the form is loaded the first time (Look up in the AddJob)



            Yeah this is where it got hard so....
            */

            return View();
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
