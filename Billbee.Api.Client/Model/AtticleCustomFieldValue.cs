using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class AtticleCustomFieldValue
    {
        public long? Id;

        public long? DefinitionId;

        public ArticleCustomFieldDefinition Definition;

        public long? ArticleId;

        public object Value;
    }
}
