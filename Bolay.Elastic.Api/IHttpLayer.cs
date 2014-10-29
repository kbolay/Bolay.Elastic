using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public interface IHttpLayer
    {
        HttpResponse Get(HttpRequest request);

        HttpResponse Post(HttpRequest request);

        HttpResponse Put(HttpRequest request);

        HttpResponse Delete(HttpRequest request);

        HttpResponse Head(HttpRequest request);
    }
}
