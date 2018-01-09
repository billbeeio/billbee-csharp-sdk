using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{

    public class TermsResult
    {
        public string LinkToTermsWebPage { get; set; }
        public string LinkToPrivacyWebPage { get; set; }
        public string LinkToTermsHtmlContent { get; set; }
        public string LinkToPrivacyHtmlContent { get; set; }
    }

}
