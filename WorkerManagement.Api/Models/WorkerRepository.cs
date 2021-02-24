using ManagementModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerManagement.Api.Models
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly AppDbContext appDbContext;

        public WorkerRepository(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<Worker> AddWorker(Worker worker)
        {
           var result = await appDbContext.Workers.AddAsync(worker);
           await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async void DeleteWorker(int workerId)
        {
            var result = await appDbContext.Workers
                 .FirstOrDefaultAsync(w => w.WorkerId == workerId);
            if (result != null)
            {
                appDbContext.Workers.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Worker> GetWorker(int workerId)
        {
            return await appDbContext.Workers
                 .FirstOrDefaultAsync(w => w.WorkerId == workerId);
        }

        public async Task<Worker> GetWorkerByEmail(string email)
        {
            return await appDbContext.Workers
            .FirstOrDefaultAsync(w => w.Email == email);
        }

        public async Task<IEnumerable<Worker>> GetWorkers()
        {
            return await appDbContext.Workers.ToListAsync();
        }

        public async Task<Worker> UpdateWorker(Worker worker)
        {
            var result = await appDbContext.Workers
            .FirstOrDefaultAsync(w => w.WorkerId == worker.WorkerId);
            if (result != null)
            {
                result.FirstName = worker.FirstName;
                result.LastName = worker.LastName;
                result.Email = worker.Email;
                result.DateOfBirth = worker.DateOfBirth;
                result.Gender = worker.Gender;
                result.DepartmentId = worker.DepartmentId;
                result.PhotoPath = worker.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
