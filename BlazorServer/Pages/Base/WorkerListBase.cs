using ManagementModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages.Base
{
    public class WorkerListBase : ComponentBase
    {
        public IEnumerable<Worker> Workers { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadWorkers();
            return base.OnInitializedAsync();
        }

        private void LoadWorkers()
        {
            Worker e1 = new Worker
            {
                WorkerId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBirth = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
                PhotoPath = "images/me2.png"
            };

            Worker e2 = new Worker
            {
                WorkerId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBirth = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = 2, DepartmentName = "HR" },
                PhotoPath = "images/om1.png"
            };

            Worker e3 = new Worker
            {
                WorkerId = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBirth = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
                PhotoPath = "images/om2.png"
            };

            Worker e4 = new Worker
            {
                WorkerId = 3,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBirth = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                Department = new Department { DepartmentId = 3, DepartmentName = "Payroll" },
                PhotoPath = "images/om3.png"
            };

            Workers = new List<Worker> { e1, e2, e3, e4 };
        }
    }
}
