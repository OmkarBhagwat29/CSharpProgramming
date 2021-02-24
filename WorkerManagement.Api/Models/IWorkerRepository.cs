using ManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerManagement.Api.Models
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetWorkers();
        Task<Worker> GetWorker(int workerId);

        Task<Worker> GetWorkerByEmail(string email);

        Task<Worker> AddWorker(Worker worker);

        Task<Worker> UpdateWorker(Worker worker);
        void DeleteWorker(int workerId);

    }
}
