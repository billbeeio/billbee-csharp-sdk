using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client
{
    /// <summary>
    /// Exception thrown, when an parametervalue was not correct or malformed.
    /// </summary>
    public class InvalidValueException: Exception
    {
        public InvalidValueException(string message): base(message)
        {

        }
    }
}
