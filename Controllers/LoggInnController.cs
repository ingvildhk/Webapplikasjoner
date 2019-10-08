using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Oppg1.Controllers
{
    public class LoggInnController : Controller
    {
        // GET: LoggInn
        public ActionResult LoggInn()
        {
            return View();
        }
    }
}