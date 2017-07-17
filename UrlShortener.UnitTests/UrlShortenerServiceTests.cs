using Moq;
using NUnit.Framework;
using System;
using UrlShortener.Core.Services;
using UrlShortener.Data.Contracts;

namespace UrlShortener.UnitTests
{
    [TestFixture]
    class UrlShortenerServiceTests
    {
        [Test]
        public void ConstructingWithoutRepositoryThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlShortenerService(null));
        }

        [Test]
        public void ConstructingWithValidParametersDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => CreateService());
        }

        [Test]
        public void ShortenLongUrlThatHasBeenShortenedReturnsTheShorten()
        {
            var service = CreateService();

            var tail = service.Shorten("http://hasBeenShortened.com");

            Assert.AreEqual("MQ==", tail);
        }

        [Test]
        public void ShortenLongUrlThatHasBeenShortenedDoesNotCauseNewAdding()
        {
            var service = CreateService();

            service.Shorten("http://hasBeenShortened.com");

            _mockRepository.Verify(x => x.Add("http://hasBeenShortened.com"), Times.Never);
        }

        [Test]
        public void ShortenLongUrlThatHasNotBeenShortenedCausesNewAdding()
        {
            var service = CreateService();

            service.Shorten("http://hasNotBeenShortened.com");

            _mockRepository.Verify(x => x.Add("http://hasNotBeenShortened.com"), Times.Once);
        }

        [Test]
        public void GetLongUrlThatHasBeenShortenedReturnsValue()
        {
            var service = CreateService();

            var longUrl = service.GetLongUrl("MQ==");

            Assert.AreEqual("http://hasBeenShortened.com", longUrl);
        }

        [Test]
        public void GetLongUrlThatHasNotBeenShortenedReturnsNull()
        {
            var service = CreateService();

            var longUrl = service.GetLongUrl("incorrect shortened url tail");

            Assert.IsNull(longUrl);
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IShortenedUrlRepository>();
            _mockRepository.Setup(x => x.GetIdOf(It.Is<string>(tail => tail == "http://hasBeenShortened.com"))).Returns(1);
            _mockRepository.Setup(x => x.GetLongUrlBy(It.Is<int>(id => id == 1))).Returns("http://hasBeenShortened.com");
        }

        private UrlShortenerService CreateService()
        {
            return new UrlShortenerService(_mockRepository.Object);
        }

        private Mock<IShortenedUrlRepository> _mockRepository;
    }
}
