using Itmo.ObjectOrientedProgramming.Lab1.RouteSections;
using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;
using Xunit;

namespace Lab1.Tests;

public class RouteSimulator_ShouldReturnFailure_WhenTheRouteStartsWithDefaultMagneticPath
{
    public static IEnumerable<object[]> TestParameters()
    {
        const double vehicle_mass = 100;
        const double vehicle_accuracy = 5;
        const double vehicle_maxPermittedAppliedPower = 500;
        var train = new Train(vehicle_mass, vehicle_accuracy, vehicle_maxPermittedAppliedPower);

        const double DefaultMagneticPath_1_Duration = 100;
        var defaultMagneticPath_1 = new DefaultMagneticPath(DefaultMagneticPath_1_Duration);

        var listOfRouteSections = new IRouteSections[] { defaultMagneticPath_1 };
        double routeLengthInKilometers = 0;
        double maxPermittedVehicleSpeedOnTheRoute = 123;
        foreach (IRouteSections section in listOfRouteSections)
        {
            routeLengthInKilometers += section.Duration;
        }

        yield return new object[] { listOfRouteSections, routeLengthInKilometers, train, maxPermittedVehicleSpeedOnTheRoute };
    }

    [Theory]
    [MemberData(nameof(TestParameters))]
    public void TestRouteForTrain(IRouteSections[] listOfRouteSections, double routeLengthInKilometers, Train train, double maxPermittedVehicleSpeedOnTheRoute)
    {
        var route = new Route(listOfRouteSections, routeLengthInKilometers, train, maxPermittedVehicleSpeedOnTheRoute);

        Assert.Equal("Неудача", route.RouteSimulator().Item1);
    }
}