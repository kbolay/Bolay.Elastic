using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public class HttpRequest
    {
        private const char _PATH_DELIMITER = '/';
        private const char _QUERY_STRING_START = '?';
        private const char _QUERY_STRING_DELIMITER = '&';
        private const char _EQUALS = '=';

        private Uri _uri;
        private IEnumerable<string> _pathPieces;
        private Dictionary<string, string> _queryString;

        public Uri Uri { get { return BuildUri(); } }
        public object Content { get; private set; }
        public Dictionary<string, string> QueryString { get { return _queryString; } }
        public Dictionary<string, string> Headers { get; private set; }

        public HttpRequest(string uri, object content = null)
            : this(new Uri(uri), content)
        { }

        public HttpRequest(Uri uri, object content = null)
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "Http Request requires a uri.");

            _uri = uri;
            Content = content;

            if (string.IsNullOrWhiteSpace(_uri.LocalPath))
            {
                _pathPieces = _uri.LocalPath.Split(new char[] { _PATH_DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
            }

            ParsePath(_uri);
            ParseQueryString(_uri);
        }

        public void ChangeUriPath(params string[] pathPieces)
        {
            _pathPieces = pathPieces.Where(x => !string.IsNullOrWhiteSpace(x));          
        }

        public void AddToQueryString(string key, string value, bool allowDefault = false, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if (value != defaultValue || allowDefault)
            {
                if (_queryString == null)
                {
                    _queryString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                _queryString[key] = value.ToString();
            }
        }

        public void RemoveFromQueryString(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if (_queryString == null || !_queryString.ContainsKey(key))
            {
                throw new Exception("The query string does not contain this key: " + key + ".");
            }

            _queryString.Remove(key);
        }

        public void AddToHeaders(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if (Headers == null)
            {
                Headers = new Dictionary<string, string>();
            }

            Headers[key] = value;
        }    

        public void RemoveFromHeaders(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            if(Headers == null || !Headers.ContainsKey(key))
            {
                throw new Exception("The headers do not contain this key: " + key + ".");
            }

            QueryString.Remove(key);
        }

        private Uri BuildUri()
        {
            StringBuilder pathBuilder = new StringBuilder();
            if (_pathPieces != null && _pathPieces.Any())
            {
                foreach (string pathPiece in _pathPieces)
                {
                    if (pathBuilder.Length > 0)
                    {
                        pathBuilder.Append(_PATH_DELIMITER);
                    }

                    pathBuilder.Append(pathPiece);
                }
            }

            StringBuilder queryStringBuilder = new StringBuilder();
            if (_queryString != null && _queryString.Any())
            {
                queryStringBuilder.Append(_QUERY_STRING_START);
                foreach (KeyValuePair<string, string> kvp in _queryString)
                {
                    if (queryStringBuilder.Length > 1)
                    {
                        queryStringBuilder.Append(_QUERY_STRING_DELIMITER);
                    }

                    queryStringBuilder.AppendFormat("{0}={1}", kvp.Key, kvp.Value);
                }
            }

            pathBuilder.Append(queryStringBuilder);

            return new Uri(_uri, pathBuilder.ToString());
        }

        private void ParsePath(Uri uri)
        {
            if (string.IsNullOrWhiteSpace(_uri.LocalPath))
            {
                _pathPieces = _uri.LocalPath.Split(new char[] { _PATH_DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        private void ParseQueryString(Uri uri)
        { 
            if(string.IsNullOrWhiteSpace(_uri.Query))
            {
                _queryString = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (string kvpStr in _uri.Query.TrimStart(new char[] { _QUERY_STRING_START }).Split(new char[] { _QUERY_STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
                { 
                    string[] pieces = kvpStr.Split(new char[] { _EQUALS }, StringSplitOptions.RemoveEmptyEntries);
                    if(pieces.Count() == 2)
                    {
                        _queryString.Add(pieces[0], pieces[1]);
                    }
                    else
                    {
                        _queryString.Add(pieces[0], null);
                    }
                }
            }
        }
    }
}
