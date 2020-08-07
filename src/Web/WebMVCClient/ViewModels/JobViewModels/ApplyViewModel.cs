using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCClient.ViewModels.JobViewModels
{
    public class ApplyViewModel
    {
        public Job Job { get; }
        public User Applicant { get; }

        public ApplyViewModel(Job job, User applicant)
        {
            Job = job;
            Applicant = applicant;
        }
    }
}
