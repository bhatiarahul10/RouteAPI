using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapgDS;

namespace RouteAPI.Exceptions
{
    public class InvalidRouteException: Exception
    {
        public InvalidRouteException() :base(Constants.ExceptionMessageForInvalidRoute)
        {
            
        }

    }
}
