using Bolay.Elastic.Api.Document.Delete;
using Bolay.Elastic.Api.Document.DeleteByQuery;
using Bolay.Elastic.Api.Document.Exist;
using Bolay.Elastic.Api.Document.Get;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.Document.MultiGet;
using Bolay.Elastic.Api.Document.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document
{
    public interface IDocumentRepository<T>
    {
        MultiGetResponse<T> MultiGet(MultiGetDocumentRequest request);
        GetResponse<T> Get(GetDocumentRequest request);
        DoesExistResponse DoesExist(DoesExistDocumentRequest request);
        IndexResponse Index(IndexDocumentRequest<T> request);
        UpdateResponse Update(UpdateDocumentRequest request);
        DeleteResponse Delete(DeleteDocumentRequest request);
        DeleteByQueryResponse DeleteByQuery(DeleteByQueryDocumentRequest request);
    }
}
