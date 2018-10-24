using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{

    public enum ConfirmationMessageSendMode
    {
        None = 0,
        Email = 1,
        Api = 2,
        EmailThenApi = 3,
        ExternalEmail = 4
    }

    /// <summary>
    /// Container to store information for a message, that can be send via an order.
    /// </summary>
    public class SendMessage
    {
        /// <summary>
        /// Defines, how the message is send
        /// </summary>
        public ConfirmationMessageSendMode SendMode = ConfirmationMessageSendMode.EmailThenApi;

        /// <summary>
        /// The Subject of the message
        /// </summary>
        public List<MultiLanguageString> Subject = new List<MultiLanguageString>();

        /// <summary>
        /// The body of the message
        /// </summary>
        public List<MultiLanguageString> Body = new List<MultiLanguageString>();

        /// <summary>
        /// An alternative recipient email address
        /// </summary>
        public string AlternativeMail = null;
    }
}
