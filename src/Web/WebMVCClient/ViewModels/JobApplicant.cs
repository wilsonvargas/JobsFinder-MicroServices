using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCClient.ViewModels
{
    public class JobApplicant
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
