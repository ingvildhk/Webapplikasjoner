using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace Oppg1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult OversiktStasjoner()
        {
            return View();
        }

        public ActionResult OversiktBaner()
        {
            return View();
        }

        public ActionResult EndreStasjon()
        {
            return View();
        }

        public ActionResult SlettStasjon()
        {
            return View();
        }

        public ActionResult EndreBane()
        {
            return View();
        }

        public ActionResult SlettBane()
        {
            return View();
        }
    }
}