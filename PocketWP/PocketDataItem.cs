using System.Runtime.Serialization;

namespace PocketWP
{
    [DataContract]
    public class PocketDataItem
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
        /// Gets the tags array.
        /// </summary>
        /// <value>
        /// The tags array.
        /// </value>
        [IgnoreDataMember]
        public string[] TagsArray
        {
            get
            {
                return string.IsNullOrEmpty(Tags) ? null : Tags.Split(',');
            }
        }
    }
}