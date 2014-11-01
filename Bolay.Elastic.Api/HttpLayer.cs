using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public class HttpLayer : IHttpLayer
    {
        private const string _APPLICATION_JSON = "application/json";

        public HttpResponse Get(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for GET.");

            if (request.Uri == null)
                throw new ArgumentNullException("uri", "GET http request requires uri.");

            try
            {
                HttpResponseMessage response = SendAsync(HttpMethod.Get, request).Result;
                return new HttpResponse(response);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (AggregateException ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message); 
            }
            catch (Exception ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.Message);
            }            
        }

        public async Task<HttpResponse> GetAsync(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for GET.");

            try
            {
                HttpResponseMessage response = await SendAsync(HttpMethod.Get, request).ConfigureAwait(false);
                return new HttpResponse(response);
            }
            catch (HttpRequestException ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (AggregateException ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return new HttpResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponse Post(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for POST.");

            if (request.Uri == null)
                throw new ArgumentNullException("uri", "POST http request requires uri.");

            HttpResponseMessage response = SendAsync(HttpMethod.Post, request).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Put(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for PUT.");

            if (request.Uri == null)
                throw new ArgumentNullException("uri", "PUT http request requires uri.");

            HttpResponseMessage response = SendAsync(HttpMethod.Put, request).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Delete(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for DELETE.");

            if (request.Uri == null)
                throw new ArgumentNullException("uri", "DELETE http request requires uri.");

            HttpResponseMessage response = SendAsync(HttpMethod.Delete, request).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Head(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for GET.");

            if (request.Uri == null)
                throw new ArgumentNullException("uri", "GET http request requires uri.");

            HttpResponseMessage response = SendAsync(HttpMethod.Head, request).Result;
            return new HttpResponse(response);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, HttpRequest request)
        {
            HttpResponseMessage response = null;

            // TODO: add authentication header info here

            using(HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(method, request.Uri);

                if(request.Content != null)
                {
                    httpRequest.Content = new StringContent(Serialize(request.Content), Encoding.UTF8, _APPLICATION_JSON);
                }

                httpRequest.AddHeaders(request.Headers);
                response = await httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            }
            
            return response;
        }

        private string Serialize(object content)
        {
            string contentStr = null;
            if (content != null)
            {
                if (content is string)
                    contentStr = content as string;
                else
                    contentStr = JsonConvert.SerializeObject(content);
            }
            return contentStr;
        }
    }
}
