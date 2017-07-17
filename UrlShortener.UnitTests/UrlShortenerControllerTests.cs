using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortener.WebApi.Contracts.Models;
using UrlShortener.WebApi.Contracts.Services;
using UrlShortener.WebApi.Controllers;

namespace UrlShortener.UnitTests
{
    [TestFixture]
    class UrlShortenerControllerTests
    {
        [Test]
        public void ConstructingWithoutServiceThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlShortenerController(null));
        }

        [Test]
        public void ConstructingWithValidParametersDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => CreateController());
        }

        [Test]
        public void PostShortenWithInvalidUrlReturnsBadRequest()
        {
            var controller = CreateController();
            controller.ModelState.AddModelError("invalid url", "error");

            var model = new UrlShorteningModel {  Url = "invalid url" };

            var response = controller.Shorten(model);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void PostShortenWithValidUrlReturnsOk()
        {
            var controller = CreateController();

            var model = new UrlShorteningModel { Url = "http://google.com" };

            var response = controller.Shorten(model);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void GetLongUrlWithIncorrectShortenedUrlTailReturnsNotFound()
        {
            var controller = CreateController();

            var response = controller.GetLongUrl("incorrectShortenedUrlTail");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void GetLongUrlWithCorrectShortenedUrlTailReturnsFound()
        {
            var controller = CreateController();

            var response = controller.GetLongUrl("correctShortenedUrlTail");

            Assert.AreEqual(HttpStatusCode.Found, response.StatusCode);
        }

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IUrlShortenerService>();
            _mockService.Setup(x => x.Shorten(It.IsAny<string>())).Returns("https://goo.gl/");
            _mockService.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns("");
            _mockService.Setup(x => x.GetLongUrl(It.Is<string>(tail => tail == "correctShortenedUrlTail"))).Returns("http://google.com");
        }

        private UrlShortenerController CreateController()
        {
            var controller = new UrlShortenerController(_mockService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            return controller;
        }

        private Mock<IUrlShortenerService> _mockService;
    }
}
