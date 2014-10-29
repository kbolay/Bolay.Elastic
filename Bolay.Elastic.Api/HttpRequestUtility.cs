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
    public class HttpRequestUtility : IHttpRequestUtility
    {
        public HttpResponse Get(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for GET.");

            if (request.RequestUri == null)
                throw new ArgumentNullException("uri", "GET http request requires uri.");

            HttpClient client = new HttpClient();
            client.AddHeaders(request.Headers);

            try
            {
                HttpResponseMessage response = client.GetAsync(request.RequestUri).Result;
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

            HttpResponse httpResponse = null;
            HttpResponseMessage httpResponseMsg = null;

            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.AddHeaders(request.Headers);
                    httpResponseMsg = client.GetAsync(request.RequestUri).Result;
                }

                return new HttpResponse(httpResponseMsg);
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

            if (request.RequestUri == null)
                throw new ArgumentNullException("uri", "POST http request requires uri.");

            HttpClient client = new HttpClient();
            client.AddHeaders(request.Headers);

            string contentStr = Serialize(request.Content);
            StringContent stringContent = new StringContent(contentStr);
            HttpResponseMessage response = client.PostAsync(request.RequestUri, stringContent).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Put(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for PUT.");

            if (request.RequestUri == null)
                throw new ArgumentNullException("uri", "PUT http request requires uri.");

            HttpClient client = new HttpClient();
            client.AddHeaders(request.Headers);

            string contentStr = Serialize(request.Content);
            StringContent stringContent = new StringContent(contentStr);
            HttpResponseMessage response = client.PutAsync(request.RequestUri, stringContent).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Delete(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for DELETE.");

            if (request.RequestUri == null)
                throw new ArgumentNullException("uri", "DELETE http request requires uri.");

            HttpClient client = new HttpClient();
            client.AddHeaders(request.Headers);

            HttpResponseMessage response = client.DeleteAsync(request.RequestUri).Result;
            return new HttpResponse(response);
        }

        public HttpResponse Head(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "HttpRequest is required for GET.");

            if (request.RequestUri == null)
                throw new ArgumentNullException("uri", "GET http request requires uri.");

            HttpClient client = new HttpClient();
            client.AddHeaders(request.Headers);

            HttpResponseMessage response = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, request.RequestUri)).Result;
            return new HttpResponse(response);
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
