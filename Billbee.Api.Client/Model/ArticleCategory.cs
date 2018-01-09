using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Information about a category of an article
    /// </summary>
    public class ArticleCategory
    {
        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The internal id of the category
        /// </summary>
        public int? Id { get; set; }
    }
}
