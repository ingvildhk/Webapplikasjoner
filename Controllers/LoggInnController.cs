using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oppg1.Models;

namespace Oppg1.Controllers
{
    public class LoggInnController : Controller
    {
        // GET: LoggInn
        public ActionResult LoggInn()
        {
            return View("LoggInn");
        }

        public ActionResult TilbakeTilIndex()
        {
            return View("Index");
        }
    }
}