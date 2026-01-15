using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Exceptions
{
    public class AppException : Exception

    {
        public int Statuscode { get; set; }
        public AppException(string message,int statuscode) : base(message)
        {
            this.Statuscode=statuscode;
        
        }
    }
}
