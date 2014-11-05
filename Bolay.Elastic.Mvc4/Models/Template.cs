using Bolay.Elastic.Api.Mapping.Models;
//using Bolay.Elastic.Mapping;
//using Bolay.Elastic.Mapping.Properties.RootObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    /// <summary>
    /// Define a template to create mapping from.
    /// </summary>
    public class Template
    {
        //private List<string> _PATHS_DEFAULT = new List<string> { "*" };
        //private IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.Analyzed;
        //private bool _STORE_DEFAULT = false;
        //private const Double _BOOST_DEFAULT = 1.0;
        //private const bool _INCLUDE_IN_ALL_DEFAULT = true;

        //private List<string> _Paths { get; set; }
        //private List<RootObjectProperty> _Types { get; set; }
        //private IndexSettingEnum _Index { get; set; }
        //private StoreSettingEnum _Store { get; set; }
        //private bool? _IncludeInAll { get; set; }
        //private Double? _Boost { get; set; }

        /// <summary>
        /// The path this template affects.
        /// Examples:
        /// * - Template is applied to all properties.
        /// object.* - Template is applied to all properies in object/array.
        /// object.property - Template is applied to the property specified.
        /// </summary>
        //public List<string> PropertyPaths
        //{
        //    get
        //    {
        //        if (_Paths != null && _Paths.Any())
        //            return _Paths;
        //        return _PATHS_DEFAULT;
        //    }
        //    set
        //    {
        //        if(value != null && value.Any(x => !string.IsNullOrWhiteSpace(x)))
        //            _Paths = new List<string>(value.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Replace("*", "")));
        //        else
        //            _Paths = null;
        //    }
        //}

        /// <summary>
        /// The elastic index type for this property.
        /// Defaults to all types.
        /// </summary>
        //public List<string> Types 
        //{
        //    get
        //    {
        //        PropertyType type = PropertyType.String;
        //        if (_Types == null || !_Types.Any())
        //            return null;
        //        return _Types.Where(x => x != null).Select(x => x.ToString()).ToList();
        //    }
        //    set
        //    {
        //        PropertyType type = PropertyType.String;
        //        if (value != null && value.Any(x => !string.IsNullOrWhiteSpace(x)))
        //            _Types = new List<PropertyType>(value.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => PropertyType.Find(x)));
        //        else
        //            _Types = null;
        //    }
        //}

        /// <summary>
        /// Include this property in the _all of this index type.
        /// Default is true.
        /// </summary>
        //public bool IncludeInAll
        //{
        //    get
        //    {
        //        if (_IncludeInAll.HasValue)
        //            return _IncludeInAll.Value;
        //        return _INCLUDE_IN_ALL_DEFAULT;
        //    }
        //    set
        //    {
        //        _IncludeInAll = value;
        //    }
        //}

        ///// <summary>
        ///// The index setting.
        ///// Default is 'analzyed'.
        ///// </summary>
        //public string Index 
        //{
        //    get
        //    {
        //        if (_Index != null)
        //            return _Index.ToString();

        //        return _INDEX_DEFAULT.ToString();
        //    }
        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //            _Index = null;

        //        _Index = IndexSettingEnum.Find(value);
        //    }
        //}

        ///// <summary>
        ///// The store setting.
        ///// Default is 'no'.
        ///// </summary>
        //public string Store
        //{
        //    get
        //    {
        //        if (_Store != null)
        //            return _Store.ToString();

        //        return _STORE_DEFAULT.ToString();
        //    }
        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //            _Store = null;

        //        _Store = StoreSettingEnum.Find(value);
        //    }
        //}

        ///// <summary>
        ///// The custom boost for this field.
        ///// Default is 1.0
        ///// </summary>
        //public Double Boost
        //{
        //    get
        //    {
        //        if (_Boost.HasValue)
        //            return _Boost.Value;

        //        return _BOOST_DEFAULT;
        //    }
        //    set
        //    {
        //        _Boost = value;
        //    }
        //}

        ///// <summary>
        ///// The analyzers applied to multifield.
        ///// </summary>
        //public List<FieldAnalyzer> Analyzers { get; set; }

        //public Template() { }
    }
}
