using Itmo.ObjectOrientedProgramming.Lab1.RouteSections;
using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;
using Xunit;

namespace Lab1.Tests;

public class RouteSimulator_ShouldReturnFailure_WhenForceMagneticPathAcceleratedVehicleMoreThanThePermissibleSpeedOfTheRouteButToThePermissibleStationAppliedSpeed
{
    public static IEnumerable<object[]> TestParameters()
    {
        const double vehicle_mass = 10;
        const double vehicle_accuracy = 5;
        const double vehicle_maxPermittedAppliedPower = 120;
        var train = new Train(vehicle_mass, vehicle_accuracy, vehicle_maxPermittedAppliedPower);

        const double ForceMagneticPath_1_Duration = 40;
        const double ForceMagneticPath_1_Power = 20;
        var forceMagneticPath_1 = new ForceMagneticPath(ForceMagneticPath_1_Duration, ForceMagneticPath_1_Power);

        const double DefaultMagneticPath_1_Duration = 100;
        var defaultMagneticPath_1 = new DefaultMagneticPath(DefaultMagneticPath_1_Duration);

        const double Station_1_CongestionLevel = 5;
        const double Station_1_MaxPermittedAppliedSpeed = 300;
        var station_1 = new Station(Station_1_CongestionLevel, Station_1_MaxPermittedAppliedSpeed);

        const double DefaultMagneticPath_2_Duration = 100;
        var defaultMagneticPath_2 = new DefaultMagneticPath(DefaultMagneticPath_2_Duration);

        var listOfRouteSections = new IRouteSections[] { forceMagneticPath_1, defaultMagneticPath_1, station_1, defaultMagneticPath_2 };
        double routeLengthInKilometers = 0;
        double maxPermittedVehicleSpeedOnTheRoute = 60;
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