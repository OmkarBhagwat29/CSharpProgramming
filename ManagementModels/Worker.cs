﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementModels
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Department Department { get; set; }
        public string PhotoPath { get; set; }

    }
}