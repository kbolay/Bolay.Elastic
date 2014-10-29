using Bolay.Elastic.Api.Document.Diagnostics;
using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Plugin
{
    public interface IElasticIndexType
    {
        Type TargetType { get; }

        /// <summary>
        /// This interface provides the concrete implementation with the Uri of the index/type.
        /// </summary>
        IUriProvider TargetIndex { get; }

        /// <summary>
        /// Total number of documents in this index/type.
        /// </summary>
        Int64 Total { get; }

        /// <summary>
        /// Start the process of populating the index.
        /// </summary>
        /// <param name="batchSize">The number of documents to process per batch.</param>
        /// <param name="maxDocuments">The maximum number of documents to place in the index.
        /// A null value indicates there is no limit.
        /// </param>
        /// <returns></returns>
        bool StartPopulatingIndex(int batchSize, Int64? maxDocuments);

        /// <summary>
        /// Cancel the population process.
        /// </summary>
        /// <returns></returns>
        bool CancelPopulationProcess();

        /// <summary>
        /// Get the number of documents that have been placed in the index during the population process.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DocumentBatch> PopulationProgress();

        /// <summary>
        /// Upload a mapping document to the elastic server
        /// </summary>
        /// <param name="mappingInfo">The mapping information.</param>
        /// <returns></returns>
        bool CreateMappingFile(string mappingInfo);

        /// <summary>
        /// Update a mapping document on the elastic server.
        /// </summary>
        /// <param name="mappingInfo">The mapping information.</param>
        /// <returns></returns>
        bool UpdateMappingFile(string mappingInfo);

        /// <summary>
        /// Retrieve a document from the index.
        /// </summary>
        /// <param name="documentId">The identifier of the document.</param>
        /// <returns></returns>
        object Get(string documentId);

        /// <summary>
        /// Retrieve a collection of documents from the index.
        /// </summary>
        /// <param name="offset">The index of the first document to retrieve.</param>
        /// <param name="size">The maximum number of documents to retrieve.</param>
        /// <returns></returns>
        IEnumerable<object> Get(int offset = 0, int size = 10);

        /// <summary>
        /// Add a document to the index/type.
        /// </summary>
        /// <param name="item">The item to add to the index.</param>
        /// <returns></returns>
        bool Create(object item);

        /// <summary>
        /// Update a document in the index/type.
        /// </summary>
        /// <param name="documentId">The identifier of the document to update.</param>
        /// <param name="item">The item to add to the index.</param>
        /// <returns></returns>
        bool Update(string documentId, object item);

        /// <summary>
        /// Remove a single document from the index.
        /// </summary>
        /// <param name="documentId">The identifier of the elastic document.</param>
        /// <returns></returns>
        bool Delete(string documentId);

        /// <summary>
        /// Attempt to delete all of the documents of this index/type.
        /// </summary>
        /// <returns></returns>
        bool DeleteAll();
    }

    public interface IElasticIndexType<T> : IElasticIndexType
    {
        /// <summary>
        /// Retrieve a document from the index.
        /// </summary>
        /// <param name="documentId">The identifier of the document.</param>
        /// <returns></returns>
        new T Get(string documentId);

        /// <summary>
        /// Retrieve a collection of documents from the index.
        /// </summary>
        /// <param name="offset">The index of the first document to retrieve.</param>
        /// <param name="size">The maximum number of documents to retrieve.</param>
        /// <returns></returns>
        new IEnumerable<T> Get(int offset = 0, int size = 10);

        /// <summary>
        /// Add a document to the index/type.
        /// </summary>
        /// <param name="item">The item to add to the index.</param>
        /// <returns></returns>
        bool Create(T item);

        /// <summary>
        /// Update a document in the index/type.
        /// </summary>
        /// <param name="documentId">The identifier of the document to update.</param>
        /// <param name="item">The item to add to the index.</param>
        /// <returns></returns>
        bool Update(string documentId, T item);
    }
}
