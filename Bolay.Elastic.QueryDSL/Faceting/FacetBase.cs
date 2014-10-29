using Bolay.Elastic.QueryDSL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    public class FacetBase : IFacet
    {
        private int _Size { get; set; }

        /// <summary>
        /// Gets the facet name.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets or sets the number of terms to return per facet.
        /// </summary>
        public int Size
        {
            get { return _Size; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Size", "Size must be greater than or equal to zero.");

                _Size = value;
            }
        }

        /// <summary>
        /// Gets or sets the nested object to facet on.
        /// Defaults to null.
        /// </summary>
        public string NestedObject { get; set; }

        /// <summary>
        /// Gets or sets the facet filter.
        /// Defaults to null.
        /// </summary>
        public IFilter FacetFilter { get; set; }

        /// <summary>
        /// Gets or sets the global value.
        /// Defaults to false.
        /// </summary>
        public bool IsScopeGlobal { get; set; }

        public FacetBase(string facetName)
        {
            if (string.IsNullOrWhiteSpace(facetName))
                throw new ArgumentNullException("facetName", "Facets require a name.");

            FacetName = facetName;
            Size = FacetSerializer._SIZE_DEFAULT;
            IsScopeGlobal = FacetSerializer._GLOBAL_DEFAULT;
        }
    }
}
