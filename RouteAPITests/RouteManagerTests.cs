using GrapgDS;
using System;
using System.Runtime.InteropServices.ComTypes;
using RouteAPI;
using RouteAPI.Exceptions;
using Xunit;

namespace RouteAPITests
{
    public class RouteManagerTests
    {
        private readonly IRouteManager _routeManager;

        public RouteManagerTests()
        {
            ILandMarkManager landMarkManager = new LandMarkManager();
            _routeManager = new RouteManager(landMarkManager);
        }

        [Fact]
        public void givenARouteRegisterIt()
        {
            var isRegisteredSuccessfully = _routeManager.RegisterRoute("A", "C", 4);
            Assert.True(isRegisteredSuccessfully);
        }
       
        [Fact]
        public void givenARouteWhenStartingAndEndingLandMarksAreSameThrowInvalidRouteException()
        {
            var exception  = Assert.Throws<InvalidRouteException>(
                ()=> _routeManager.RegisterRoute("A", "A", 4));
            Assert.Equal(Constants.ExceptionMessageForInvalidRoute, exception.Message);
        }

        [Fact]
        public void givenARegisteredRouteGetDistanceAssociatedWithIt()
        {
            _routeManager.RegisterRoute("A", "B", 3);
            _routeManager.RegisterRoute("B", "C", 4);
            var distance  = _routeManager.GetDistance("ABC");
            Assert.Equal(7,distance);
        }

        [Fact]
        public void givenARouteWhenRouteAlreadyExistsThrowRouteAlreadyExistsException()
        {
            var isRegisteredSuccessfully = _routeManager.RegisterRoute("A", "B", 4);
            Assert.True(isRegisteredSuccessfully);

            Assert.Throws<RouteAlreadyExistsException>(()=> 
                _routeManager.RegisterRoute("A", "B", 4));
        }

        [Fact]
        public void givenARouteGetDifferentLandMarks()
        {
         
        }

        [Fact]
        public void givenTwoLandMarksWithALimitOfMaximumStopsReturnTheProbableRoutes()
        {
           
        }

        [Fact]
        public void givenTwoLandMarksWhenNoPathExistsThenReturnLiteralStringPathNotExists()
        {
            Assert.Throws<RouteDoesNotExistException>(() => _routeManager.GetDistance("XYZ"));
        }

    }
}
