using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PocketWP
{
    public static class PocketHelper
    {
        private const string PocketScheme = "pocket:";
        private const string PocketUrl = PocketScheme + "Add?item=";

        /// <summary>
        /// Adds a single item to Pocket
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="title">The title.</param>
        /// <param name="tweetId">The tweet identifier.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after adding.</param>
        public static void AddItemToPocket(Uri uri, string tags = null, string title = null, string tweetId = null, string callbackUri = null)
        {
            AddItemToPocket(uri.ToString(), tags, title, tweetId, callbackUri);
        }

        /// <summary>
        /// Adds a single item to Pocket
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="title">The title.</param>
        /// <param name="tweetId">The tweet identifier.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after adding.</param>
        public static void AddItemToPocket(string uri, string tags = null, string title = null, string tweetId = null, string callbackUri = null)
        {
            AddToPocket(new PocketData
            {
                Items = new List<PocketDataItem>()
                {
                    new PocketDataItem
                    {
                        Uri = uri,
                        Tags = tags,
                        Title = title,
                        TweetId = tweetId
                    }
                },
                CallbackUri = callbackUri
            });
        }

        /// <summary>
        /// Adds multiple items to Pocket
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after adding.</param>
        public static void AddItemsToPocket(List<PocketDataItem> items, string callbackUri = null)
        {
            AddToPocket(new PocketData
            {
                Items = items,
                CallbackUri = callbackUri
            });
        }

        /// <summary>
        /// Launch Pocket application URI with appended pocket data.
        /// </summary>
        /// <param name="data">The pocket data.</param>
        /// <exception cref="System.ArgumentNullException">item;Your nut can't be null</exception>
        private static async void AddToPocket(PocketData data)
        {
            if (data == null || data.Items == null)
            {
                throw new ArgumentNullException("data", "Your item can't be null");
            }

            var url = PocketUrl + data.ToEscapedJson();

            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }

        /// <summary>
        /// Determines whether the specified URI contains pocket data.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True if pocket data is present</returns>
        public static bool HasPocketData(Uri uri)
        {
            return uri.ToString().Contains(Uri.EscapeDataString(PocketUrl));
        }

        /// <summary>
        /// Retrieves the pocket data.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The deserialised pocket data</returns>
        public static PocketData RetrievePocketData(Uri uri)
        {
            var pocketUri = uri.ToString().Replace("/Protocol?encodedLaunchUri=", string.Empty);
            pocketUri = Uri.UnescapeDataString(pocketUri).Replace(PocketUrl, string.Empty);

            var itemJson = Uri.UnescapeDataString(pocketUri);

            try
            {
                var serializer = new DataContractJsonSerializer(typeof(PocketData));
                using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(itemJson)))
                {
                    return (PocketData)serializer.ReadObject(reader);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
