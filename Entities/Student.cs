using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI.Entities
{
    public class Student
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field {0} is required")]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(maximumLength: 15)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field {0} is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(maximumLength: 50)]
        public string Career { get; set; }

    }
}
