using System;
using System.Collections.Generic;
using System.Text;

namespace Billbee.Api.Client.Model
{
    public class OrderMessage
    {
        /// <summary>
        /// Sets the message as internal if true
        /// </summary>
        public bool IsInternal { get; set; }
        
        /// <summary>
        /// The name of the sender
        /// </summary>
        public string SenderName { get; set; }
        
        /// <summary>
        /// The text of the message
        /// </summary>
        public string Text { get; set; }
    }
}
