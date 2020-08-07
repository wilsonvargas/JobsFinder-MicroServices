using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCClient.ViewModels.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
    }
}
