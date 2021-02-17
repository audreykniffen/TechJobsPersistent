using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string Name { get; set; }

        public int EmployerId { get; set; }

        public List<SelectListItem> Employer { get; set; }

        public List<Skill> Skills { get; set; }

        public int SkillId { get; set; }


        public AddJobViewModel(List<Employer> employers, List<Skill> possibleSkills)
        {
            Employer = new List<SelectListItem>();

            foreach (var employer in employers)
            {
                Employer.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
            Skills = possibleSkills;
        }

        public AddJobViewModel()
        {
        }
    }
}

/*  
 Create properties for the job Name, the EmployerId, and a List<SelectListItem> as a list of all the employers

 You'll eventually need similar for the skills (But they won't be a select list) and their Id

 You'll also need a constructor to create this AddJobViewModel (look at AddJobSkillViewModel) 

 */
