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
    }
}
