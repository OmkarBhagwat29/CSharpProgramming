using ManagementModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerManagement.Api.Models;

namespace WorkerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController: ControllerBase
    {
        private readonly IWorkerRepository workerRepo;

        public WorkersController(IWorkerRepository workerRepo)
        {
            this.workerRepo = workerRepo;
        }


        [HttpGet]
        public async Task<ActionResult> GetWorkers()
        {
            try
            {
                return Ok(await workerRepo.GetWorkers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            } 
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            try
            {
               var result = await workerRepo.GetWorker(id);

                if (result == null)
                    return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Worker>> CreateWorker(Worker w)
        {
            try
            {
                if (w == null)
                    return BadRequest();

                var result = await workerRepo.GetWorkerByEmail(w.Email);

                if (result != null)
                {
                    ModelState.AddModelError("Email", "eamil address is alread in use");
                    return BadRequest();
                }

                result = await workerRepo.AddWorker(w);


                return CreatedAtAction(nameof(GetWorker),
                    new { id = result.WorkerId }, result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
          
        }
    }
}
