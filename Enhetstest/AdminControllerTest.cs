using System;
using System.Collections.Generic;
using BLL;
using DAL;
using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oppg1.Controllers;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using MvcContrib.TestHelper;

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
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Innlogget"] = true;

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
            var bane = new bane()
            {
                BaneID = 0
            };

            // Act
            var actionResult = (ViewResult)controller.EndreBane(0, bane);

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
            var avgang = new stasjonPaaBane();
            avgang.Avgang = "";

            // Act
            var actionResult = (ViewResult)controller.EndreAvgang(avgang.stasjonPaaBaneID, avgang);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreAvgang_EndreAvgang_Feil()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjonPaaBane = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 0
            };

            // Act
            var actionResult = (ViewResult)controller.EndreAvgang(0, stasjonPaaBane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettStasjon()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.SlettStasjon(1);
            var resultat = (stasjon)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");


        }
        [TestMethod]
        public void SlettStasjon_SlettOK_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettStasjon(1, stasjon);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktStasjoner");
        }
        [TestMethod]
        public void SlettStasjon_SlettOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettStasjon(0, stasjon);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettBane()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.SlettBane(1);
            var resultat = (bane)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");


        }
        [TestMethod]
        public void SlettBane_SlettOK_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBane(1, bane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktBaner");
        }
        [TestMethod]
        public void SlettBane_SlettOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettBane(0, bane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettAvgang()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.SlettAvgang(1);
            var resultat = (stasjonPaaBane)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");


        }
        [TestMethod]
        public void SlettAvgang_SlettOK_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var avgang = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                BaneID = 1,
                Bane = "L1",
                StasjonsID = 1,
                Stasjon = "Oslo S",
                Avgang = "12:00"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettAvgang(1, avgang);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "OversiktStasjoner");
        }
        [TestMethod]
        public void SlettAvgang_SlettOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var avgang = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                BaneID = 1,
                Bane = "L1",
                StasjonsID = 1,
                Stasjon = "Oslo S",
                Avgang = "12:00"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettAvgang(0, avgang);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilStasjon()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.LeggTilStasjon();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilStasjon_LeggTil_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };
            // Act
            var result = (RedirectToRouteResult)controller.LeggTilStasjon(stasjon);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "OversiktStasjoner");
        }
        [TestMethod]
        public void LeggTilStasjon_Model_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon();
            controller.ViewData.ModelState.AddModelError("Stasjonsnavn", "Stasjonsnavn må oppgis");

            // Act
            var actionResult = (ViewResult)controller.LeggTilStasjon(stasjon);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void LeggTilStasjon_LeggTilOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjon = new stasjon();
            stasjon.Stasjonsnavn = "";

            // Act
            var actionResult = (ViewResult)controller.LeggTilStasjon(stasjon);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilBane()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            // Act
            var actionResult = (ViewResult)controller.LeggTilBane();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilBane_LeggTil_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };
            // Act
            var result = (RedirectToRouteResult)controller.LeggTilBane(bane);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "OversiktBaner");
        }
        [TestMethod]
        public void LeggTilBane_Model_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane();
            controller.ViewData.ModelState.AddModelError("Banenavn", "Banenavn må oppgis");

            // Act
            var actionResult = (ViewResult)controller.LeggTilBane(bane);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void LeggTilBane_LeggTilOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var bane = new bane();
            bane.Banenavn = "";

            // Act
            var actionResult = (ViewResult)controller.LeggTilBane(bane);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilAvgang()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var id = 1;

            // Act
            var actionResult = (ViewResult)controller.LeggTilAvgang(id);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LeggTilAvgang_LeggTil_OK()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));

            var avgang = new stasjonPaaBane()
            {
                stasjonPaaBaneID = 1,
                BaneID = 1,
                Bane = "L1",
                StasjonsID = 1,
                Stasjon = "Oslo S",
                Avgang = "12:00"
            };
            // Act
            var result = (RedirectToRouteResult)controller.LeggTilAvgang(avgang);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "OversiktStasjoner");
        }
        [TestMethod]
        public void LeggTilAvgang_Model_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var avgang = new stasjonPaaBane();
            controller.ViewData.ModelState.AddModelError("Tidspunkt", "Tidspunkt må oppgis");

            // Act
            var actionResult = (ViewResult)controller.LeggTilAvgang(avgang);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void LeggTilAvgang_LeggTilOK_Feil()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var avgang = new stasjonPaaBane();
            avgang.Avgang = "";

            // Act
            var actionResult = (ViewResult)controller.LeggTilAvgang(avgang);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void HentAlleStasjoner()
        {
            // Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var stasjonListe = new List<stasjon>();
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(stasjonListe);

            // Act
            var resultat = controller.hentAlleStasjoner();

            // Assert
            for (var i = 0; i < resultat.Length; i++)
            {
                Assert.AreEqual(forventetResultat[i], resultat[i]);
            }
        }


        [TestMethod]
        public void HentAlleBanenavn()
        {
            //Arrange
            var controller = new AdminController(new VyBLL(new AdminDBMetoderStubs()));
            var baneListe = new List<bane>();
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };
            baneListe.Add(bane);
            baneListe.Add(bane);
            baneListe.Add(bane);
            baneListe.Add(bane);

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(baneListe);

            // Act
            var resultat = controller.hentAlleBanenavn();

            // Assert
            for (var i = 0; i < resultat.Length; i++)
            {
                Assert.AreEqual(forventetResultat[i], resultat[i]);
            }
        }
    }
}
