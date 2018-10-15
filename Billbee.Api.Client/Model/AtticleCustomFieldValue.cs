using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class AtticleCustomFieldValue
    {
        public int? Id;

        public int? DefinitionId;

        public ArticleCustomFieldDefinition Definition;

        public int? ArticleId;

        public object Value;
    }
}
