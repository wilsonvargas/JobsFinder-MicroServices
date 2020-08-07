using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCClient.ViewModels.JobViewModels
{
    public class JobApplicationViewModel
    {
        public User Applicant { get; set; }
        public Job Job { get; set; }
    }
}
