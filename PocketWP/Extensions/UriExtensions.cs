using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PocketWP.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Gets a collection of query string values.
        /// </summary>
        /// <param name="uri">The current uri.</param>
        /// <returns>A collection that contains the query string values.</returns>
        public static IDictionary<string, string> QueryString(this Uri uri)
        {
            var uriString = uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString;

            var queryIndex = uriString.IndexOf("?", StringComparison.InvariantCulture);

            if (queryIndex == -1)
            {
                return new Dictionary<string, string>();
            }

            var query = uriString.Substring(queryIndex + 1);

            return query.Split('&')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split('='))
                .ToDictionary(x => HttpUtility.UrlDecode(x[0]), x => x.Length == 2 && !string.IsNullOrEmpty(x[1]) ? HttpUtility.UrlDecode(x[1]) : null);
        }
    }
}
