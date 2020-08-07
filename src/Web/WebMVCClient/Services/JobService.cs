using Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVCClient.Config;
using WebMVCClient.Services.Interfaces;
using WebMVCClient.ViewModels;

namespace WebMVCClient.Services
{
    public class JobService : IJobService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;

        public JobService(IHttpClient httpClient, ApiConfig apiConfig)
        {
            _apiClient = httpClient;
            _apiConfig = apiConfig;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var dataString = await _apiClient.GetStringAsync(_apiConfig.JobsApiUrl + "/api/jobs");
            return JsonConvert.DeserializeObject<IEnumerable<Job>>(dataString);
        }

        public async Task<Job> GetJob(int jobId)
        {
            var dataString = await _apiClient.GetStringAsync(_apiConfig.JobsApiUrl + "/api/jobs/" + jobId);
            return JsonConvert.DeserializeObject<Job>(dataString);
        }

        public async Task AddApplicant(JobApplicant jobApplicant)
        {
            var response = await _apiClient.PostAsync(_apiConfig.JobsApiUrl + "/api/jobs/applicants", jobApplicant);
            response.EnsureSuccessStatusCode();
        }
    }
}
