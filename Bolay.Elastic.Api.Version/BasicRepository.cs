using Bolay.Elastic.Api.Basic;
using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Version
{
    public class BasicRepository : IBasicRepository
    {
        private readonly Uri _ServerUri;
        private readonly IHttpRequestUtility _HttpRequestUtility;

        public BasicRepository(IUriProvider serverUri, IHttpRequestUtility httpRequestUtility)
        {
            if (serverUri == null || serverUri.Uri == null)
                throw new ArgumentNullException("serverUri", "Version Repository requires the uri of the ES server.");

            _ServerUri = new Uri(serverUri.Uri.GetLeftPart(UriPartial.Authority));
            _HttpRequestUtility = httpRequestUtility;
        }

        public Basic.Models.BasicInformation Get()
        {
            throw new NotImplementedException();

            HttpResponse response = _HttpRequestUtility.Get(new HttpRequest(_ServerUri));
            
        }
    }
}
