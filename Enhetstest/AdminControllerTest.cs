using System;
using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oppg1.Controllers;

namespace Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void OversiktStasjoner()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void OversiktBaner()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void AvgangerPaStasjon()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void LeggTilAvgang()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void EndreStasjon()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void EndreBane()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void EndreAvgang()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void SlettStasjon()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void SlettBane()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void LeggTilStasjon()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }

        [TestMethod]
        public void LeggTilBane()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }
    }
}
