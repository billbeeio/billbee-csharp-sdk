using System;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Comment for usage with orders
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// If true, this comment was send from the customer, otherwise it was send from the shop owner
        /// </summary>
        public bool FromCustomer { get; set; }

        /// <summary>
        /// The comment itself
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The name of the sender
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date, when this commment was published
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The internal id of this comment
        /// </summary>
        public int Id { get; set; }
    }
}
