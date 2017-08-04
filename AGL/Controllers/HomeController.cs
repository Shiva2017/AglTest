using AGL.Models;
using AGL.Orchestration;
using AGL.Orchestration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AGL.Controllers
{
    public class HomeController : Controller
    {
        IProcessAglData _processAglData;
        public HomeController(IProcessAglData processAglData)
        {
            _processAglData = processAglData;
        }
        public async Task<ActionResult> AGL()
        {
            var aglData = await _processAglData.Process();
            return View("AGL", aglData);
        }
         
    }
}