﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            AddToPocket(new ExternalPocketItem
            {
                Uri = uri,
                Tags = tags,
                Title = title,
                TweetId = tweetId,
                CallbackUri = callbackUri,
                Type = AddType.Single
            });
        }

        /// <summary>
        /// Adds multiple items to Pocket
        /// </summary>
        /// <param name="urls">The URIs.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="callbackUri">The callback URI for your app if you want to be called back after adding.</param>
        public static void AddItemsToPocket(List<string> urls, string tags = null, string callbackUri = null)
        {
            AddToPocket(new ExternalPocketItem
            {
                Urls = urls,
                Tags = tags,
                CallbackUri = callbackUri,
                Type = AddType.Multiple
            });
        }

        /// <summary>
        /// Buries my nut.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">item;Your nut can't be null</exception>
        private static async void AddToPocket(ExternalPocketItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Your item can't be null");
            }

            var url = PocketUrl + item.ToEscapedJson();

            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }

        /// <summary>
        /// Determines whether the specified URI has nuts.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True if nut is present</returns>
        public static bool HasPocketItem(Uri uri)
        {
            // /Protocol?encodedLaunchUri=squirrel%3AAddNut%3Fnut%3D
            return uri.ToString().Contains(HttpUtility.UrlEncode(PocketUrl));
        }

        /// <summary>
        /// Retrieves the nut.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The deserialised nut</returns>
        public static ExternalPocketItem RetrievePocketItem(Uri uri)
        {
            var pocketUri = uri.ToString().Replace("/Protocol?encodedLaunchUri=", string.Empty);
            pocketUri = Uri.UnescapeDataString(pocketUri).Replace(PocketUrl, string.Empty);

            var itemJson = Uri.UnescapeDataString(pocketUri);

            try
            {
                var serializer = new DataContractJsonSerializer(typeof(ExternalPocketItem));
                using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(itemJson)))
                {
                    return (ExternalPocketItem)serializer.ReadObject(reader);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
