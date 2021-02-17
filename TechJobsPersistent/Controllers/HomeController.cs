﻿using System;
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
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> possibleSkills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, possibleSkills);

            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    Name = addJobViewModel.Name,
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = context.Employers.Find(addJobViewModel.EmployerId),
                };

                for (int i = 0; i < selectedSkills.Length; i++)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        JobId = job.Id,
                        Job = job,
                        SkillId = Int32.Parse(selectedSkills[i]),
                    };
                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("Index");
            }

            return View("Add", addJobViewModel);
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
/* So this will be kinda Like ProcessAddEmployerForm that you did in the Employer Controller

This action needs to take in an instance of the AddJobViewModel and

If the model is valid then
    create a Job object that 
        will pass the properties (Name, EmployerId, and Employer) from the ViewModel that it's taking in.

        - remember that the Employer is an employer object, so we have to search for that object in the Db using the EmployerId


     When we add our Skills, we will need to iterate across all of the selected skills - a string array - (cause we can shoose more than one skill per job)
        Create a new JobSkill with the properties of
            JobId being set to the ID of the new Job we're creating
            Job being set to the new Job we're creating,
            SkillID being set to the value of the skill we are currently looking at in the array (remember what type of array is it, that will probably need handled)
        Add this new skill to the JobSkills Db


    Then we need to add this new Job object that we've crated to the Jobs table in the Db
    Then save the changes to the Db

    when all that works we'll redirect to see the list of Jobs   

If none of that happens (cause the model isn't valid), then we want to go to the Add view and 
pass it the information that is in the View Model from the attempted addition

*/

