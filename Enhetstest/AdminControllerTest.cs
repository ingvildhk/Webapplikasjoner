using System;
using System.Collections.Generic;
using BLL;
using DAL;
using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oppg1.Controllers;
using System.Web.Mvc;
using System.Linq;

namespace Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void OversiktStasjoner()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new List<stasjon>();
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };
            forventetResultat.Add(stasjon);
            forventetResultat.Add(stasjon);
            forventetResultat.Add(stasjon);
            forventetResultat.Add(stasjon);

            //Act
            var actionResult = (ViewResult)controller.OversiktStasjoner();
            var resultat = (List<stasjon>)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].StasjonID, resultat[i].StasjonID);
                Assert.AreEqual(forventetResultat[i].Stasjonsnavn, resultat[i].Stasjonsnavn);
            }
        }

        [TestMethod]
        public void OversiktBaner()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new List<bane>();
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };
            forventetResultat.Add(bane);
            forventetResultat.Add(bane);
            forventetResultat.Add(bane);
            forventetResultat.Add(bane);

            //Act
            var actionResult = (ViewResult)controller.OversiktBaner();
            var resultat = (List<bane>)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].BaneID, resultat[i].BaneID);
                Assert.AreEqual(forventetResultat[i].Banenavn, resultat[i].Banenavn);
            }
        }

        [TestMethod]
        public void AvgangerPaStasjon()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new List<stasjonPaaBane>();
            var stasjonPaaBane = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                StasjonsID = 1,
                Stasjon = "Oslo S",
                BaneID = 1,
                Bane = "L1",
                Avgang = "12:00"
            };
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);

            //Act
            var actionResult = (ViewResult)controller.AvgangerPaStasjon(stasjonPaaBane.stasjonPaaBaneID);
            var resultat = (List<stasjonPaaBane>)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].stasjonPaaBaneID, resultat[i].stasjonPaaBaneID);
                Assert.AreEqual(forventetResultat[i].StasjonsID, resultat[i].StasjonsID);
                Assert.AreEqual(forventetResultat[i].Stasjon, resultat[i].Stasjon);
                Assert.AreEqual(forventetResultat[i].BaneID, resultat[i].BaneID);
                Assert.AreEqual(forventetResultat[i].Bane, resultat[i].Bane);
                Assert.AreEqual(forventetResultat[i].Avgang, resultat[i].Avgang);
            }
        }

        [TestMethod]
        public void AvgangerPaStasjon_ID_0()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new List<stasjonPaaBane>();
            var stasjonPaaBane = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 0
            };
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);
            forventetResultat.Add(stasjonPaaBane);

            //Act
            var actionResult = (ViewResult)controller.AvgangerPaStasjon(stasjonPaaBane.stasjonPaaBaneID);
            var resultat = (List<stasjonPaaBane>)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].stasjonPaaBaneID, resultat[i].stasjonPaaBaneID);
                Assert.AreEqual(forventetResultat[i].StasjonsID, resultat[i].StasjonsID);
                Assert.AreEqual(forventetResultat[i].Stasjon, resultat[i].Stasjon);
                Assert.AreEqual(forventetResultat[i].BaneID, resultat[i].BaneID);
                Assert.AreEqual(forventetResultat[i].Bane, resultat[i].Bane);
                Assert.AreEqual(forventetResultat[i].Avgang, resultat[i].Avgang);
            }
        }

        [TestMethod]
        public void EndreStasjon()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };

            //Act
            var actionResult = (ViewResult)controller.EndreStasjon(forventetResultat.StasjonID);
            var resultat = (stasjon)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.StasjonID, resultat.StasjonID);
            Assert.AreEqual(forventetResultat.Stasjonsnavn, resultat.Stasjonsnavn);
        }

        [TestMethod]
        public void EndreStasjon_ID_0()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new stasjon()
            {
                StasjonID = 0
            };

            //Act
            var actionResult = (ViewResult)controller.EndreStasjon(forventetResultat.StasjonID);
            var resultat = (stasjon)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.StasjonID, resultat.StasjonID);
            Assert.AreEqual(forventetResultat.Stasjonsnavn, resultat.Stasjonsnavn);
        }

        [TestMethod]
        public void EndreStasjon_Post_OK()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreStasjon(stasjon.StasjonID, stasjon);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktStasjoner");
        }

        [TestMethod]
        public void EndreStasjon_Model_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var stasjon = new stasjon();
            controller.ViewData.ModelState.AddModelError("Stasjonsnavn", "Stasjonsnavn må oppgis");

            // Act
            var actionResult = (ViewResult)controller.EndreStasjon(stasjon.StasjonID, stasjon);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreStasjon_StasjonOK_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon();
            stasjon.Stasjonsnavn = "";

            // Act
            var actionResult = (ViewResult)controller.EndreStasjon(stasjon.StasjonID, stasjon);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreStasjon_EndreStasjon_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.EndreStasjon(0);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBane()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };

            //Act
            var actionResult = (ViewResult)controller.EndreBane(forventetResultat.BaneID);
            var resultat = (bane)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.BaneID, resultat.BaneID);
            Assert.AreEqual(forventetResultat.Banenavn, resultat.Banenavn);
        }

        [TestMethod]
        public void EndreBane_ID_0()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new bane()
            {
                BaneID = 0
            };

            //Act
            var actionResult = (ViewResult)controller.EndreBane(forventetResultat.BaneID);
            var resultat = (bane)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.BaneID, resultat.BaneID);
            Assert.AreEqual(forventetResultat.Banenavn, resultat.Banenavn);
        }

        [TestMethod]
        public void EndreBane_Post_OK()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreBane(bane.BaneID, bane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktBaner");
        }

        [TestMethod]
        public void EndreBane_Model_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var bane = new bane();
            controller.ViewData.ModelState.AddModelError("Banenavn", "Banenavn må oppgis");

            // Act
            var actionResult = (ViewResult)controller.EndreBane(bane.BaneID, bane);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBane_BaneOK_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane();
            bane.Banenavn = "";

            // Act
            var actionResult = (ViewResult)controller.EndreBane(bane.BaneID, bane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBane_EndreBane_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.EndreBane(0);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreAvgang()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                BaneID = 1,
                Bane = "L1",
                StasjonsID = 1,
                Stasjon = "Oslo S",
                Avgang = "12:00"
            };

            //Act
            var actionResult = (ViewResult)controller.EndreAvgang(forventetResultat.BaneID);
            var resultat = (stasjonPaaBane)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.stasjonPaaBaneID, resultat.stasjonPaaBaneID);
            Assert.AreEqual(forventetResultat.BaneID, resultat.BaneID);
            Assert.AreEqual(forventetResultat.Bane, resultat.Bane);
            Assert.AreEqual(forventetResultat.StasjonsID, resultat.StasjonsID);
            Assert.AreEqual(forventetResultat.Stasjon, resultat.Stasjon);
            Assert.AreEqual(forventetResultat.Avgang, resultat.Avgang);
        }

        [TestMethod]
        public void EndreAvgang_ID_0()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var forventetResultat = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 0
            };

            //Act
            var actionResult = (ViewResult)controller.EndreAvgang(forventetResultat.stasjonPaaBaneID);
            var resultat = (stasjonPaaBane)actionResult.Model;

            //Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.stasjonPaaBaneID, resultat.stasjonPaaBaneID);
            Assert.AreEqual(forventetResultat.BaneID, resultat.BaneID);
            Assert.AreEqual(forventetResultat.Bane, resultat.Bane);
            Assert.AreEqual(forventetResultat.StasjonsID, resultat.StasjonsID);
            Assert.AreEqual(forventetResultat.Stasjon, resultat.Stasjon);
            Assert.AreEqual(forventetResultat.Avgang, resultat.Avgang);
        }

        [TestMethod]
        public void EndreAvgang_Post_OK()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjonPaaBane = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                BaneID = 1,
                Bane = "L1",
                StasjonsID = 1,
                Stasjon = "Oslo S",
                Avgang = "12:00"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EndreAvgang(stasjonPaaBane.stasjonPaaBaneID, stasjonPaaBane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktStasjoner");
        }

        [TestMethod]
        public void EndreAvgang_Model_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var stasjonPaaBane = new stasjonPaaBane();
            controller.ViewData.ModelState.AddModelError("Tidspunkt", "Tidspunkt må oppgis");

            // Act
            var actionResult = (ViewResult)controller.EndreAvgang(stasjonPaaBane.stasjonPaaBaneID, stasjonPaaBane);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreAvgang_Tidspunkt_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var stasjonPaaBane = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                Avgang = "14.000"
            };

            // Act
            var actionResult = (ViewResult)controller.EndreAvgang(stasjonPaaBane.stasjonPaaBaneID, stasjonPaaBane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreAvgang_AvgangOK_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane();
            bane.Banenavn = "";

            // Act
            var actionResult = (ViewResult)controller.EndreBane(bane.BaneID, bane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreAvgang_EndreAvgang_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.EndreBane(0);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
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
        public void SlettAvgang()
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

        [TestMethod]
        public void LeggTilAvgang()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.LeggTilAvgang();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void HentAlleStasjoner()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }


        [TestMethod]
        public void HentAlleBaner()
        {
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
        }
    }
}
