using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public enum ApiArticleCustomFieldType
    {
        TextField,
        Textarea,
        NumberInput,
        SelectInput,
    }

    public class ArticleCustomFieldDefinition
    {
        public long? Id;

        public string Name;

        public object Configuration;

        public ApiArticleCustomFieldType? Type;

        public bool IsNullable;
    }
}
