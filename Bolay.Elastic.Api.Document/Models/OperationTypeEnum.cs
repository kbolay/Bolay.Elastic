using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public sealed class OperationTypeEnum : TypeSafeEnumBase<OperationTypeEnum>
    {
        /// <summary>
        /// If a document with the same id already exists for this type the create operation will fail.
        /// </summary>
        public static readonly OperationTypeEnum Create = new OperationTypeEnum("create");

        /// <summary>
        /// Index the document, this may create or update an existing document.
        /// </summary>
        public static readonly OperationTypeEnum Index = new OperationTypeEnum("index");

        /// <summary>
        /// 
        /// </summary>
        public static readonly OperationTypeEnum Update = new OperationTypeEnum("update");



        private OperationTypeEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
