using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "Error: Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Error: Location is required")]
        public string Location { get; set; }

        // Add validation for both properties so they are both required fields
        //Look at AddJobSkillViewModel if you're struggling with these


    }
}