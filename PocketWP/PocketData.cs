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
    public class PocketData
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [DataMember]
        public PocketDataItem Item { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember]
        public List<PocketDataItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the callback URI for your app if you want to be called back after adding.
        /// </summary>
        /// <value>
        /// The callback URI.
        /// </value>
        [DataMember]
        public string CallbackUri { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// Single/Multiple items.
        /// </value>
        [DataMember]
        public AddType Type { get; internal set; }

        /// <summary>
        /// Converts POCO to JSON.
        /// </summary>
        /// <returns></returns>
        public string ToEscapedJson()
        {
            var serialiser = new DataContractJsonSerializer(typeof(PocketData));
            using (var stream = new MemoryStream())
            {
                serialiser.WriteObject(stream, this);

                var json = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
                return System.Uri.EscapeDataString(json);
            }
        }
    }
}
