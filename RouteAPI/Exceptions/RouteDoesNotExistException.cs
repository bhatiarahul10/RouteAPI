using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapgDS;

namespace RouteAPI.Exceptions
{
    public class RouteDoesNotExistException:Exception
    {
        public RouteDoesNotExistException():base(Constants.ExceptionMessageWhenRouteDoesNotExists)
        {
            
        }
    }
}
