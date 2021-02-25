using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MorseCodeServer.Models;
using MorseCodeServer.Services;
using System;
using System.Threading.Tasks;

namespace MorseCodeServer.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserInputModel _model;
        private IMorseDecoder _decoder;
        private IRequestHandler _handler;

        public ServicesController(IRequestHandler handler, IMorseDecoder decoder, ILogger<HomeController> logger)
        {
            _model = new UserInputModel();
            _decoder = decoder;
            _handler = handler;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<int> GetFrequency()
        {
            string freq = Request.Cookies["sound"];
            if (freq != null)
            {
                _model.Frequency = int.Parse(freq);
                return _model.Frequency;
            }
            else
            {
                return _model.Frequency;
            }
        }

        [Route("setup")]
        [HttpGet]
        public ActionResult Setup(int sound)
        {
            var frequency = FrequencyChanger.ChangeFrequency(sound);
            Response.Cookies.Append("sound", frequency.ToString());
            _model.Frequency = frequency;
            return View("~/Views/Home/Index.cshtml", _model);
        }

        [Route("morse")]
        [HttpGet]
        public async Task<ActionResult<string>> Morse(string msg)
        {
          
            var res = await _handler.Handle(msg);
            var decodedInput = res;
            ViewBag.Input = decodedInput.ToString();

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        public void Log(string input)
        {
            _logger.LogInformation(input);
            //get last log and check size if not max.
            //if max create new file.

        }
    }
}
