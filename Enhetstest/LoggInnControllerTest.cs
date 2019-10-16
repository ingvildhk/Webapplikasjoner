using System;
using System.Collections.Generic;
using BLL;
using DAL;
using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oppg1.Controllers;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;

namespace Enhetstest
{
    [TestClass]
    public class LoggInnControllerTest
    {
        [TestMethod]
        public void LoggInn()
        {
            //Arrange
            var controller = new LoggInnController(new VyBLL(new AdminDBMetoderStubs()));
            var SessionMock= new TestControllerBuilder();
            
            SessionMock.InitializeController(controller);
            controller.Session["Innlogget"] = false;
            // Act
            var result = (ViewResult)controller.LoggInn();
            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void LoggInn_BrukerFinnes_OK()
        {
            //Arrange
            var controller = new LoggInnController(new VyBLL(new AdminDBMetoderStubs()));
            var SessionMock = new TestControllerBuilder();
            var bruker = new bruker()
            {
                Brukernavn = "navn"
            };

            SessionMock.InitializeController(controller);
            controller.Session["Innlogget"] = true;
            // Act
            var actionResult = (RedirectToRouteResult)controller.LoggInn(bruker);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktStasjoner");
        }

        [TestMethod]
        public void LoggInn_BrukerFinnes_Feil()
        {
            //Arrange
            var controller = new LoggInnController(new VyBLL(new AdminDBMetoderStubs()));
            var SessionMock = new TestControllerBuilder();
            var bruker = new bruker();
            bruker.Brukernavn = "";

            SessionMock.InitializeController(controller);
            controller.Session["Innlogget"] = false;
            // Act
            var result = (ViewResult)controller.LoggInn(bruker);
            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void LoggUt()
        {
            //Arrange
            var controller = new LoggInnController(new VyBLL(new AdminDBMetoderStubs()));
            var SessionMock = new TestControllerBuilder();

            SessionMock.InitializeController(controller);
            controller.Session["Innlogget"] = false;
            // Act
            var actionResult = (RedirectToRouteResult)controller.LoggUt();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "../Home/Index");
        }
    }
}
