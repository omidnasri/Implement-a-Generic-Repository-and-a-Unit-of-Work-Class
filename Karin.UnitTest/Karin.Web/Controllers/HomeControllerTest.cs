using System.Web.Mvc;
using Karin.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Karin.UnitTest.Karin.Web.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            if (result != null) Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
