using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using theworldcore.Model;
using theworldcore.Services;
using theworldcore.ViewModels;

namespace theworldcore.Controllers.Web
{
    public class AppController: Controller
    {
        private readonly IMessageSender messageSender;
        private readonly IConfigurationRoot config;
        private readonly IWorldRepository worldRepository;

        public AppController(IMessageSender messageSender, IConfigurationRoot config, IWorldRepository worldRepository)
        {
            this.messageSender = messageSender;
            this.config = config;
            this.worldRepository = worldRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            //var trips = worldRepository.GetAllTrips();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (contactViewModel.Email.EndsWith("aol.com"))
            {
                ModelState.AddModelError("Email", "Not valid email");
            }
            if (ModelState.IsValid)
            {
                ViewBag.Log = $"Name: {contactViewModel.Name}";
                var senderEmail = config["MessageConfig:FromAddress"];
                messageSender.SendMessage("me@aol.com", contactViewModel.Email, contactViewModel.Message);
            }

            return View();
        }
    }
}
