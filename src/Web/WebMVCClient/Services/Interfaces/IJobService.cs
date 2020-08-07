using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVCClient.ViewModels;

namespace WebMVCClient.Services.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<Job> GetJob(int jobId);
        Task AddApplicant(JobApplicant jobApplicant);
    }
}
