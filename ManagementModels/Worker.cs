using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagementModels
{
    public class Worker
    {
        public int WorkerId { get; set; }
        
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }
        
        public string PhotoPath { get; set; }

    }
}
