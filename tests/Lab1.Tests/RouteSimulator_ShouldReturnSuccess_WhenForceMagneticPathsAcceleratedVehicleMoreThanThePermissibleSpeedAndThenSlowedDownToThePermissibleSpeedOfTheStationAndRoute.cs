using Itmo.ObjectOrientedProgramming.Lab1.RouteSections;
using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;
using Xunit;

namespace Lab1.Tests;

public class RouteSimulator_ShouldReturnSuccess_WhenForceMagneticPathsAcceleratedVehicleMoreThanThePermissibleSpeedAndThenSlowedDownToThePermissibleSpeedOfTheStationAndRoute
{
    public static IEnumerable<object[]> TestParameters()
    {
        const double vehicle_mass = 10;
        const double vehicle_accuracy = 5;
        const double vehicle_maxPermittedAppliedPower = 120;
        var train = new Train(vehicle_mass, vehicle_accuracy, vehicle_maxPermittedAppliedPower);

        const double ForceMagneticPath_1_Duration = 40;
        const double ForceMagneticPath_1_Power = 50;
        var forceMagneticPath_1 = new ForceMagneticPath(ForceMagneticPath_1_Duration, ForceMagneticPath_1_Power);

        const double DefaultMagneticPath_1_Duration = 20;
        var defaultMagneticPath_1 = new DefaultMagneticPath(DefaultMagneticPath_1_Duration);

        const double ForceMagneticPath_2_Duration = 40;
        const double ForceMagneticPath_2_Power = -30;
        var forceMagneticPath_2 = new ForceMagneticPath(ForceMagneticPath_2_Duration, ForceMagneticPath_2_Power);

        const double Station_1_CongestionLevel = 5;
        const double Station_1_MaxPermittedAppliedSpeed = 190;
        var station_1 = new Station(Station_1_CongestionLevel, Station_1_MaxPermittedAppliedSpeed);

        const double DefaultMagneticPath_2_Duration = 20;
        var defaultMagneticPath_2 = new DefaultMagneticPath(DefaultMagneticPath_2_Duration);

        const double ForceMagneticPath_3_Duration = 40;
        const double ForceMagneticPath_3_Power = 100;
        var forceMagneticPath_3 = new ForceMagneticPath(ForceMagneticPath_3_Duration, ForceMagneticPath_3_Power);

        const double DefaultMagneticPath_3_Duration = 20;
        var defaultMagneticPath_3 = new DefaultMagneticPath(DefaultMagneticPath_3_Duration);

        const double ForceMagneticPath_4_Duration = 40;
        const double ForceMagneticPath_4_Power = -100;
        var forceMagneticPath_4 = new ForceMagneticPath(ForceMagneticPath_4_Duration, ForceMagneticPath_4_Power);

        var listOfRouteSections = new IRouteSections[] { forceMagneticPath_1, defaultMagneticPath_1, forceMagneticPath_2, station_1, defaultMagneticPath_2, forceMagneticPath_3, defaultMagneticPath_3, forceMagneticPath_4 };
        double routeLengthInKilometers = 0;
        double maxPermittedVehicleSpeedOnTheRoute = 500;
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

        Assert.Equal("Удача", route.RouteSimulator().Item1);
    }
}