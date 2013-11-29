using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using PropertyChanged;

namespace PocketWP
{
    [ImplementPropertyChanged]
    [DataContract]
    public class ExternalPocketItem
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [DataMember]
        public string Uri { get; set; }
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [DataMember]
        public string Tags { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the tweet identifier.
        /// </summary>
        /// <value>
        /// The tweet identifier.
        /// </value>
        [DataMember]
        public string TweetId { get; set; }
        /// <summary>
        /// Gets or sets the callback URI for your app if you want to be called back after adding.
        /// </summary>
        /// <value>
        /// The callback URI.
        /// </value>
        public string CallbackUri { get; set; }
        [DataMember]
        public AddType Type { get; internal set; }
        [DataMember]
        public List<string> Urls { get; set; }
        
        [IgnoreDataMember]
        public string[] TagsArray
        {
            get
            {
                return string.IsNullOrEmpty(Tags) ? null : Tags.Split(',');
            }
        }

        public string ToEscapedJson()
        {
            var serialiser = new DataContractJsonSerializer(typeof(ExternalPocketItem));
            using (var stream = new MemoryStream())
            {
                serialiser.WriteObject(stream, this);

                var json = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
                return System.Uri.EscapeDataString(json);
            }
        }
    }
}
