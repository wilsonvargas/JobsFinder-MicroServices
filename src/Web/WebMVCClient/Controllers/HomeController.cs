using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMVCClient.Models;
using WebMVCClient.Services.Interfaces;
using WebMVCClient.ViewModels.HomeViewModels;

namespace WebMVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobService _jobService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel { Jobs = await _jobService.GetJobs() });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
