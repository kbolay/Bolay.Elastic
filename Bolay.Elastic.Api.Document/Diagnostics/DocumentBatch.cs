using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Diagnostics
{
    public class DocumentBatch
    {
        public IEnumerable<DocumentReport> Batch { get; set; }
        public IEnumerable<TimeSplice> TimeSplices { get; set; }
        public DateTime? Started 
        { 
            get 
            {
                if (TimeSplices == null || !TimeSplices.Any())
                    return null;

                var orderedSplices = TimeSplices.OrderBy(x => x.Started);
                return orderedSplices.First().Started;
            } 
        }
        public Int64 TotalMilliseconds
        {
            get
            {
                if (TimeSplices == null || !TimeSplices.Any())
                    return 0;

                return TimeSplices.Sum(x => x.TotalMilliseconds);
            }
        }

        public DocumentBatch(IEnumerable<DocumentReport> reports, IEnumerable<TimeSplice> splices)
        {
            if (reports == null || !reports.Any())
                throw new ArgumentNullException("reports");

            Batch = reports;
            TimeSplices = splices;
        }
    }
}
