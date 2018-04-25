using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using content.Models;
using Microsoft.AspNetCore.Authorization;
using content.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace content.Controllers
{
    public class HomeController : Controller
    {


        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;

        public HomeController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _configuration = configuration;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
        }
        [Authorize]
        public IActionResult Index()
        {
            var identifier = User.FindFirst(Startup.ObjectIdentifierType)?.Value;

            // Initialize the GraphServiceClient.
            var graphClient = _graphSdkHelper.GetAuthenticatedClient(identifier);

            var me = graphClient.Me.Request().GetAsync().Result;
            ViewBag.me = me;

            var messages = graphClient.Me.Messages.Request().GetAsync().Result;
            ViewBag.messages = messages;

            var files = graphClient.Me.Drive.Root.Children.Request().GetAsync().Result;
            ViewBag.files = files;


            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
