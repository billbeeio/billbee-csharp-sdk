using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Image information for an article image
    /// </summary>
    public class ArticleImage
    {
        /// <summary>
        ///  The url, this image is located
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The id of this image
        /// </summary>
        public int Id { get; set; }

        public string ThumbPathExt { get; set; }
        /// <summary>
        /// Url of the thumbail
        /// </summary>
        public string ThumbUrl { get; set; }
        /// <summary>
        /// If more than one image is given, the position defines the order.
        /// </summary>
        public byte? Position { get; set; }
        /// <summary>
        /// Defines, wether this is default image, or not.
        /// </summary>
        public bool? IsDefault { get; set; }
    }
}
