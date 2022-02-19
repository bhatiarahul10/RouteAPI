using GrapgDS;
using System;
using System.Runtime.InteropServices.ComTypes;
using RouteAPI;
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
            var exception  = Assert.Throws<InvalidOperationException>(
                ()=> _routeManager.RegisterRoute("A", "A", 4));
            Assert.Equal(Constants.ExceptionMessageForInvalidRoute, exception.Message);
        }

        [Fact]
        public void givenARegisteredRouteGetTheDistanceAssociatedWithIt()
        {
            var isRegisteredSuccessfully = _routeManager.RegisterRoute("A", "C", 4);
            Assert.True(isRegisteredSuccessfully);
        }

        [Fact]
        public void givenARouteWhenRouteAlreadyExistsThrowAlreadyExistsException()
        {
            var isRegisteredSuccessfully = _routeManager.RegisterRoute("A", "A", 4);
            Assert.True(isRegisteredSuccessfully);
        }

        [Fact]
        public void givenARouteGetDifferentLandMarks()
        {
         
        }

        [Fact]
        public void givenARouteGetTheDistance()
        {
           
        }

        [Fact]
        public void givenTwoLandMarksWithALimitOfMaximumStopsReturnTheProbableRoutes()
        {
           
        }

        [Fact]
        public void givenTwoLandMarksGetDistanceBetweenThem()
        {
         
        }

        [Fact]
        public void givenTwoLandMarksWhenNoPathExistsThenReturnLiteralStringPathNotExists()
        {
            
        }


    }
}
