using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Exceptions
{
    [DataContract]
    public class IdentificationExeption
    {
        public IdentificationExeption(string message)
        {
            Message = message;
        }
        [DataMember]
        public string Message
        {
            get;
            private set;
        }
    }
}
