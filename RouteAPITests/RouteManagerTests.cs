using GrapgDS;
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
            var exception = Assert.Throws<RouteException>(
                () => _routeManager.RegisterRoute("A", "A", 4));
            Assert.Equal(Constants.ExceptionMessageForInvalidRoute, exception.Message);
        }

        [Fact]
        public void givenARegisteredRouteGetDistanceAssociatedWithIt()
        {
            _routeManager.RegisterRoute("A", "B", 3);
            _routeManager.RegisterRoute("B", "C", 4);
            var distance = _routeManager.GetDistance("A-B-C");
            Assert.Equal(7, distance);
        }

        [Fact]
        public void givenAInvalidRouteToGetDistanceThenThrowRouteDoesNotExistException()
        {
            var exception = Assert.Throws<RouteException>(() => _routeManager.GetDistance("A-A-C"));
            Assert.Equal(Constants.ExceptionMessageWhenRouteDoesNotExists, exception.Message);

            exception = Assert.Throws<RouteException>(() => _routeManager.GetDistance("A"));
            Assert.Equal(Constants.ExceptionMessageWhenRouteDoesNotExists, exception.Message);
        }

        [Fact]
        public void givenARouteWhenRouteAlreadyExistsThrowRouteAlreadyExistsException()
        {
            var isRegisteredSuccessfully = _routeManager.RegisterRoute("A", "B", 4);
            Assert.True(isRegisteredSuccessfully);

            var exception = Assert.Throws<RouteException>(() =>
                _routeManager.RegisterRoute("A", "B", 4));
            Assert.Equal(Constants.ExceptionMessageWhenRouteAlreadyExists,exception.Message);
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
            var exception = Assert.Throws<RouteException>(() => _routeManager.GetDistance("XYZ"));
            Assert.Equal(Constants.ExceptionMessageWhenRouteDoesNotExists, exception.Message);
        }

        [Fact]
        public void givenTwoLandMarksGetTheRoute()
        {
            _routeManager.RegisterRoute("A", "B", 4);
            _routeManager.RegisterRoute("A", "C", 4);
            _routeManager.RegisterRoute("B", "D", 4);
            _routeManager.RegisterRoute("C", "E", 4);
            _routeManager.RegisterRoute("C", "F", 4);
            _routeManager.RegisterRoute("E", "G", 4);
            _routeManager.RegisterRoute("D", "P", 4);
            _routeManager.RegisterRoute("P", "R", 4);
            _routeManager.RegisterRoute("R", "G", 4);

            var noOfRoutes = _routeManager.GetRoutesForLandMarksWithSpecifiedNumberOfHops("A", "G", 4);
            Assert.Equal(1, noOfRoutes);

        }

    }
}
