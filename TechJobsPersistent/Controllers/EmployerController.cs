using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }
        // create a private DbContext 
        // pass it into an EmployerController constructor
        //Look at the Search Controller if you get stumped on this one


        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }
        // pass ALL the Employer objects in teh Db into the view
        // Look at the SkillController Index to see if you can borrow some code from there.

        public IActionResult Add()
        {
            Employer employer = new Employer();
            return View(employer);

            // make a new AddEmployerViewModel instance 
            // make sure your View is going to the form (cshtml named the same as this action)  
            // that will need this AddEmployerViewModel
            // Peek at the Skills Controller again. It won't look exactly the same, but it's the same idea. 
        }

        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Employer employer = new Employer
                {
                    Name = viewModel.Name,
                    Location = viewModel.Location
                };

                context.Employers.Add(employer);
                context.SaveChanges();
                return Redirect("/Employer");
            }

            return View("Add", viewModel);
        }

     
     
            // Ok there's nowhere we can kinda directly copy code from to make this one happen. So I'm gonna walk it through
            // just like I'd be live coding it.

            /*
              This action will need to take in an AddEmployerViewModel and 

              IF the model is valid then 
                  We need to create an Employer object that
                     will pass the properties (Name and Location) from the ViewModel that it's taking in

                   Then we need to add this new Employer object that we've crated to the Employers table in the Db
                   Then save the changes to the Db
                   when all that works we'll redirect to see the list of Employers

              If none of that happens (cause the model isn't valid), then we want to go to the Add view and 
              pass it the information that is in the View Model from the attempted addition


               - - - - 
               PARTS of this are very similar to the [Post] Add in Skills but that's about as close as we get
               */


    

    public IActionResult About(int id)
        {
            Employer employer = context.Employers.Find(id);
            /* There is an About in Skills... but what's there is overkill for what we need here. 
                But we can take a look at it and understand what it's doing right? (Comments continued on the Skills Controller)

                ok... so we're passing this About an int like the Skill Controller, but I've told you that search is overkill.
                but lets start with making an Employer object to hold the one we're looking for. 
                and we want to set this object to the Db Employer table

                now when we type "context.Employer." we're gonna get some options. There's one really near the top of that list
                that will take in a parameter and search for it in the Db..... do we think this might help??? 
            */

            return View(employer);
        }
    }
}
