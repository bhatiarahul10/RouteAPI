using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GrapgDS;

namespace RouteAPI.Exceptions
{
    public class RouteException : Exception
    {
        public RouteException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public int StatusCode { get; }
    }
}
