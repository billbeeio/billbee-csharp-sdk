using Billbee.Api.Client.Enums;

namespace Billbee.Api.Client.Model
{
    public class LayoutTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ReportTemplates Type { get; set; }
    }
}