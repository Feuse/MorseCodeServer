using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MorseCodeServer.Models;
using MorseCodeServer.Services;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MorseCodeServer.Controllers
{
    public class ServicesController : Controller
    {
     
        private UserInputModeldyn _model;
        private IMorseDecoder _decoder;
        private IRequestHandler _handler;
        private NLog.Logger _logger;
        private FrequencyChanger _freq;
        private IMemoryCache _cache;
        public ServicesController(IRequestHandler handler, IMorseDecoder decoder, ILogger<HomeController> logger,IMemoryCache cache)
        {

            _cache = cache;
            _freq = new FrequencyChanger(cache);
            _model = new UserInputModeldyn();
            _model.Frequency = UserInputModel.Frequency;
            //_model.Input = UserInputModel.Input;
            _decoder = decoder;
            _handler = handler;
            _logger = NLog.LogManager.GetLogger("Morse");
        }

        [HttpGet]
        public ActionResult<UserInputModeldyn> GetFrequency()
        {
            return _model;
        }

        [Route("setup")]
        [HttpGet]
        public ActionResult Setup(int sound)
        {
            _freq.ChangeFrequency(audio, sound);

            return View("~/Views/Home/Index.cshtml", _model);
        }
        static byte[] audio;

        [Route("getSound")]
        //[HttpPost]
        public async Task<byte[]> GetSound(string msg)
        {
            audio = _decoder.MorseBuilder(msg);

            await Task.Delay(1000000);
            return audio;
        }

        [Route("morse")]
        //[HttpPost]
        public ActionResult Morse(string msg)
        {
           _model.message = msg;
           _model.morse = _decoder.Decode(msg);

            return View("~/Views/Home/Index.cshtml", _model);
        }

        [Route("logMessage")]
        [HttpGet]
        public void LogMessage(string morse, string text)
        {
            _logger.Log(NLog.LogLevel.Info, "text: " + text + " morse: " + morse);
            //get last log and check size if not max.
            //if max create new file.
            //Nlog has that functionallity built in.

        }
        [Route("log")]
        [HttpGet]
        public ActionResult<string[]> Log(int n)
        { 
            var lines =  System.IO.File.ReadLines(ConfigurationManager.AppSettings["logFile"]);
           return lines.TakeLast(n).ToArray();
        }
        [Route("timeout")]
        public string Error()
        {
            
            return "server has timed out";
        }
    }
}
