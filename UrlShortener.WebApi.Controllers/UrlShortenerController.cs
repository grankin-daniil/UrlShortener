using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortener.WebApi.Contracts.Models;
using UrlShortener.WebApi.Contracts.Services;

namespace UrlShortener.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UrlShortenerController : ApiController
    {
        private readonly IUrlShortenerService _urlShortenerService;

        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            Contract.Requires<ArgumentNullException>(urlShortenerService != null);

            _urlShortenerService = urlShortenerService;
        }

        [HttpPost]
        [Route("shorten")]
        public HttpResponseMessage Shorten(UrlShorteningModel model)
        {
            if (ModelState.IsValid)
            {
                var shortenedUrlTail = _urlShortenerService.Shorten(model.Url);
                var shortenedUrlRoot = Request.RequestUri.AbsoluteUri.Substring(0, Request.RequestUri.AbsoluteUri.IndexOf(Request.RequestUri.AbsolutePath));
                return Request.CreateResponse(HttpStatusCode.OK, $"{shortenedUrlRoot}/{shortenedUrlTail}");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpGet]
        [Route("~/{shortenedUrlTail}")]
        public HttpResponseMessage GetLongUrl(string shortenedUrlTail)
        {
            var longUrl = _urlShortenerService.GetLongUrl(shortenedUrlTail);
            if (string.IsNullOrEmpty(longUrl))
                return Request.CreateResponse(HttpStatusCode.NotFound);
            var response = Request.CreateResponse(HttpStatusCode.Found);
            response.Headers.Location = new Uri(longUrl);
            return response;
        }
    }
}
