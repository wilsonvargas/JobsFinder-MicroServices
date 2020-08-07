using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCClient.ViewModels.JobViewModels
{
    public class ApplySuccessViewModel
    {
        public Job Job { get; }
        public long RecentApplicantCount { get; }

        public ApplySuccessViewModel(Job job, long recentApplicantCount)
        {
            Job = job;
            RecentApplicantCount = recentApplicantCount;
        }
    }
}
